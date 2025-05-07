using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BrainsToDo.Repositories;

public class UserSignUpRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public enum PasswordStrength
    {
        Weak,
        Medium,
        Strong
    }

    public async Task<(User user, Mail verificationMail, PasswordStrength strength)> CreateUserWithVerification(
        string username,
        string password,
        string email,
        string verificationCode)
    {
        
        // Validate inputs
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Username, password and email cannot be empty");
        }

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
        {
            throw new ArgumentException("Username contains invalid characters");
        }
        
        // Check if username l already exists
        if (await _context.User.AnyAsync(u => u.Name == username))
        {
            throw new InvalidOperationException("Username already exists");
        }
        
        // Check password strength
        var passwordStrength = EvaluatePasswordStrength(password);
        if (passwordStrength == PasswordStrength.Weak)
        {
            throw new ArgumentException(
                "Password is too weak. It should contain at least 8 characters with a mix of uppercase, lowercase, numbers and special characters");
        }
        
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email format");
        }
        
        // Check if email l already exists
        if (await _context.Mail.AnyAsync(m => m.Email == email))
        {
            throw new InvalidOperationException("Email already in use");
        }
        
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = new User
            {
                Name = username,
                Password = password,
                EmailConfirmed = false,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            // Create verification email
            var mail = new Mail
            {
                Email = email,
                Code = verificationCode,
                ExpirationTime = DateTime.UtcNow.AddHours(24),
                UserId = user.Id,
                Attempts = 3,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };

            await _context.Mail.AddAsync(mail);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return (user, mail, passwordStrength);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private PasswordStrength EvaluatePasswordStrength(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return PasswordStrength.Weak;

        int score = 0;

        // Length contributes to score
        if (password.Length >= 8) score++;
        if (password.Length >= 12) score++;

        // Complexity features
        if (Regex.IsMatch(password, "[A-Z]")) score++; // Uppercase letters
        if (Regex.IsMatch(password, "[a-z]")) score++; // Lowercase letters
        if (Regex.IsMatch(password, "[0-9]")) score++; // Numbers
        if (Regex.IsMatch(password, "[^a-zA-Z0-9]")) score++; // Special chars

        return score switch
        {
            <= 2 => PasswordStrength.Weak, // Basic or no complexity
            <= 4 => PasswordStrength.Medium, // Moderate complexity
            _ => PasswordStrength.Strong // High complexity
        };
    }
    
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}