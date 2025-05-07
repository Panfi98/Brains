using BrainsToDo.Data;
using BrainsToDo.Models;
using BrainsToDo.Interfaces;

namespace BrainsToDo.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailVerificationService> _logger;
    private readonly DataContext _context;


    public EmailVerificationService(IEmailSender emailSender, ILogger<EmailVerificationService> logger,
        DataContext context)
    {
        _emailSender = emailSender;
        _logger = logger;
        _context = context;
    }

    public async Task<string> GenerateAndSendVerificationCodeAsync(User user)
    {

        var code = new Random().Next(100000, 999999).ToString();
        var expirationTime = DateTime.UtcNow.AddMinutes(15);


        /*user.Code = code;
        user.ExpirationTime = expirationTime;
        user.Attempts = 3;
        user.updatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();


        {
            await _emailSender.SendEmailAsync(
                user.Email,
                "Varification code",
                $"Your code: {code}");

            _logger.LogInformation($"Verification code sent to {user.Email}");
            return code;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send verification code to {user.Email}");
            throw;
        }*/
        return code;
    }

    public async Task<bool> VerifyCodeAsync(User user, string code)
    {
        /*if (user.ExpirationTime < DateTime.UtcNow)
        {
            return false;
        }

        if (user.Attempts <= 0)
        {
            return false;
        }

        if (user.Code != code)
        {

            user.Attempts--;
            user.updatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return false;
        }


        user.Code = null;
        user.ExpirationTime = DateTime.MinValue;
        user.updatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();*/

        return true;

    }
}