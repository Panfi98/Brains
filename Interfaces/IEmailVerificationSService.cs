using BrainsToDo.Models;

namespace BrainsToDo.Interfaces;

public interface IEmailVerificationService
{
    Task<string> GenerateAndSendVerificationCodeAsync(User user);
    Task<bool> VerifyCodeAsync(User user, string code);
}