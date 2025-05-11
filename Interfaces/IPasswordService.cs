using Microsoft.AspNetCore.Identity;
using BrainsToDo.Models;

namespace BrainsToDo.Interfaces;

public interface IPasswordService
{
    string HashPassword(User user, string password);
    PasswordVerificationResult VerifyPassword(User user, string hashedPassword, string providedPassword);
}