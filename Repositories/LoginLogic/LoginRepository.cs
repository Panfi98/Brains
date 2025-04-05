using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Repositories.LoginLogic;

public class LoginRepository(DataContext context)
{
    private readonly DataContext _context = context;
    public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        var users = await _context.User.ToListAsync();
        User? user = users.FirstOrDefault(u => u.Name == username && u.Password == password);
        return user;
    }
}