using BrainsToDo.Data;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BrainsToDo.Repositories;

public class UserLogInRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Username and password cannot be empty");
        }

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
        {
            throw new ArgumentException("Username contains invalid characters");
        }

        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long");
        }

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
}