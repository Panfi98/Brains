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