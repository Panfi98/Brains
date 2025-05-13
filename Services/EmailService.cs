using MailKit.Net.Smtp;
using MimeKit;
using BrainsToDo.Interfaces;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendVerificationEmailAsync(string email, string verificationCode)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Registration verification";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<p>Your verefication code: <strong>{verificationCode}</strong></p>" +
                       "<p>Your code is valid for 15 minutes.</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(
            emailSettings["MailServer"], 
            int.Parse(emailSettings["MailPort"]), 
            bool.Parse(emailSettings["UseSsl"]));

        await client.AuthenticateAsync(
            emailSettings["SenderEmail"], 
            emailSettings["Password"]);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}