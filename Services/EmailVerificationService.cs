using BrainsToDo.Data;
using BrainsToDo.Models;

namespace BrainsToDo.Services;


public interface IEmailVerificationService
{
    Task<string> GenerateAndSendVerificationCodeAsync(User user);
    Task<bool> VerifyCodeAsync(User user, string code);
}

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailVerificationService> _logger;
    private readonly DataContext _context;

    public EmailVerificationService(
        IEmailSender emailSender, 
        ILogger<EmailVerificationService> logger,
        DataContext context)
    {
        _emailSender = emailSender;
        _logger = logger;
        _context = context;
    }

    public async Task<string> GenerateAndSendVerificationCodeAsync(User user)
    {
        // Генерация 6-значного кода
        var code = new Random().Next(100000, 999999).ToString();
        var expirationTime = DateTime.UtcNow.AddMinutes(15);

        // Обновляем данные пользователя
        user.Code = code;
        user.ExpirationTime = expirationTime;
        user.Attempts = 3; // Сбрасываем количество попыток
        user.updatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Отправка email
        try
        {
            await _emailSender.SendEmailAsync(
                user.Email,
                "Код подтверждения регистрации",
                $"Ваш код подтверждения: {code}");
            
            _logger.LogInformation($"Verification code sent to {user.Email}");
            return code;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send verification code to {user.Email}");
            throw;
        }
    }

    public async Task<bool> VerifyCodeAsync(User user, string code)
    {
        if (user.ExpirationTime < DateTime.UtcNow)
        {
            return false; // Код устарел
        }

        if (user.Attempts <= 0)
        {
            return false; // Превышено количество попыток
        }

        if (user.Code != code)
        {
            // Уменьшаем количество попыток
            user.Attempts--;
            user.updatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return false;
        }

        // Код верный - очищаем данные верификации
        user.Code = null;
        user.ExpirationTime = DateTime.MinValue;
        user.updatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }
}