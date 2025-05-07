using MailKit.Net.Smtp;
using MimeKit;
using BrainsToDo.Interfaces;

public class EmailSender : IEmailSender
{
    private readonly string _senderName;
    private readonly string _senderEmail;
    private readonly string _password;
    private readonly string _mailServer;
    private readonly int _mailPort;
    private readonly bool _useSsl;

    public EmailSender(IConfiguration configuration)
    {
        _senderName = configuration["Email:SenderName"];
        _senderEmail = configuration["Email:SenderEmail"];
        _password = configuration["Email:Password"];
        _mailServer = configuration["Email:MailServer"];
        _mailPort = int.Parse(configuration["Email:MailPort"]);
        _useSsl = bool.Parse(configuration["Email:UseSsl"]);
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_senderName, _senderEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using var client = new SmtpClient();
        await client.ConnectAsync(_mailServer, _mailPort, _useSsl);
        await client.AuthenticateAsync(_senderEmail, _password);
    }
}