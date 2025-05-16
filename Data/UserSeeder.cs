using BrainsToDo.Data;
using BrainsToDo.Interfaces;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BrainsToDo.Services
{
    public class UserSeeder
    {
        private readonly DataContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<UserSeeder> _logger;

        public UserSeeder(DataContext context, IPasswordService passwordService, ILogger<UserSeeder> logger)
        {
            _context = context;
            _passwordService = passwordService;
            _logger = logger;
        }

        public async Task SeedUsersAsync()
        {
            if (!await _context.User.AnyAsync())
            {
                _logger.LogInformation("No users found in database. Starting user seeding...");
                
                string adminPasswordHash = _passwordService.HashPassword(null, "Admin123!");
                string testUserPasswordHash = _passwordService.HashPassword(null, "User123!");

                var adminUser = new User
                {
                    Name = "admin@example.com", 
                    Password = adminPasswordHash,
                    EmailConfirmed = true
                };

                var testUser = new User
                {
                    Name = "user@example.com", 
                    Password = testUserPasswordHash,
                    EmailConfirmed = true
                };

                await _context.User.AddRangeAsync(adminUser, testUser);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully seeded users to the database.");
            }
            else
            {
                _logger.LogInformation("Users already exist in the database. Skipping seeding.");
            }
        }
    }
}