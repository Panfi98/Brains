using BrainsToDo.Data;
using BrainsToDo.Models;
using BrainsToDo.Enums;
using BrainsToDo.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BrainsToDo.Repositories;

public class UserRepository(DataContext context)
{
    private readonly DataContext _context = context;
    
    //LogIn
    public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        try
        {
            var userExists = await _context.User
                .AsNoTracking()
                .AnyAsync(u => u.Name == username);
                
            if (!userExists)
            {
                return null; 
            }
            
            var user = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == username && u.Password == password);

            return user; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving user: {ex.Message}");
            throw; 
        }
    }
    
    //SignUp
    
    public async Task<(User user, Mail verificationMail, EnumPasswordStrength strength)> 
        CreateUserWithVerification(
        string username,
        string password,
        string email)
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
        
        // Check if username  already exists
        if (await _context.User.AnyAsync(u => u.Name == username))
        {
            throw new InvalidOperationException("Username already exists");
        }
        
        // Check password strength
        var passwordStrength = EvaluatePasswordStrength(password);
        if (passwordStrength == EnumPasswordStrength.Weak)
        {
            throw new ArgumentException(
                "Password is too weak. It should contain at least 8 characters with a mix of uppercase, lowercase, numbers and special characters");
        }
        
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email format");
        }
        
        // Check if email  already exists
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

            var verificationCode = new Random().Next(100000, 999999).ToString();
            
            var mail = new Mail
            {
                Email = email,
                Code = verificationCode,
                ExpirationTime = DateTime.UtcNow.AddMinutes(15),
                UserId = user.Id,
                Attempts = 3,
                IsActive = true,
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

    private EnumPasswordStrength EvaluatePasswordStrength(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return EnumPasswordStrength.Weak;

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
            <= 2 => EnumPasswordStrength.Weak, // Basic or no complexity
            <= 4 => EnumPasswordStrength.Medium, // Moderate complexity
            _ => EnumPasswordStrength.Strong // High complexity
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
    
    public async Task<VerificationResult> VerifyEmailCodeAsync(int userId, string code)
    {
        var result = new VerificationResult();
    
        var mailRecord = await _context.Mail
            .FirstOrDefaultAsync(m => m.UserId == userId && m.IsActive);

        if (mailRecord == null) 
        {
            result.ErrorMessage = "Verification record not found or inactive";
            return result;
        }

        if (DateTime.UtcNow > mailRecord.ExpirationTime)
        {
            mailRecord.IsActive = false;
            await _context.SaveChangesAsync();
            result.ErrorMessage = "Verification time expired. Please request a new code.";
            return result;
        }

        if (mailRecord.Code != code)
        {
            mailRecord.Attempts--;
        
            if (mailRecord.Attempts <= 0)
            {
                mailRecord.IsActive = false;
                await _context.SaveChangesAsync();
                result.ErrorMessage = "No attempts remaining. Please request a new code.";
                return result;
            }

            await _context.SaveChangesAsync();
            result.ErrorMessage = $"Invalid code. Attempts left: {mailRecord.Attempts}";
            return result;
        }

        var user = await _context.User.FindAsync(userId);
        if (user != null)
        {
            user.EmailConfirmed = true;
            mailRecord.IsActive = false;
            await _context.SaveChangesAsync();
            result.IsSuccess = true;
            result.User = user;
        }

        return result;
    }
    public async Task<(bool success, string message)> ResendVerificationCode(int userId, string email)
    {
        // Deactivate the old codes for user 
        var oldRecords = await _context.Mail
            .Where(m => m.UserId == userId && m.IsActive)
            .ToListAsync();

        foreach (var record in oldRecords)
        {
            record.IsActive = false;
        }

        // create new record 
        var newCode = new Random().Next(100000, 999999).ToString();
        var mail = new Mail
        {
            Email = email,
            Code = newCode,
            ExpirationTime = DateTime.UtcNow.AddMinutes(15),
            UserId = userId,
            Attempts = 3,
            IsActive = true,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        };

        await _context.Mail.AddAsync(mail);
        await _context.SaveChangesAsync();

        return (true, "New verification code sent");
    }
    
    public async Task<(User? user, Mail? mail)> GetUserWithLatestMailAsync(int userId)
    {
        var user = await _context.User.FindAsync(userId);
        if (user == null)
        {
            return (null, null);
        }

        var mail = await _context.Mail
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.createdAt)
            .FirstOrDefaultAsync();

        return (user, mail);
    }
    public class VerificationResult
    {
        public bool IsSuccess { get; set; }
        public User? User { get; set; }
        public string? ErrorMessage { get; set; }
    }
    
    public object CreateSuccessResponse(string message, object? data = null)
    {
        return new 
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
    
    public object CreateErrorResponse(string message)
    {
        return new 
        {
            Success = false,
            Message = message
        };
    }
    
    public Payload<object> CreateErrorPayload(string message, string status = "Error")
    {
        return new Payload<object>
        {
            RequestStatus = status,
            Message = message
        };
    }

    public PayloadList<T> CreateSuccessPayloadList<T>(T data, string message = "Success")
    {
        return new PayloadList<T>
        {
            Data = data,
            Message = message
        };
    }
}
