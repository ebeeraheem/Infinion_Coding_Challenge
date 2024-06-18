using Infinion.Application.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Infinion.Application.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        // Generate email
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(
            _config.GetValue<string>("AppSettings:AppName"),
            _config.GetValue<string>("EmailSettings:From")));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };

        // Send email
        using var client = new SmtpClient();
        await client.ConnectAsync(
            _config.GetValue<string>("EmailSettings:SmtpServer"),
            // Port is not null
            int.Parse(_config.GetValue<string>("EmailSettings:Port")!),
            SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(
            _config.GetValue<string>("EmailSettings:Username"),
            _config.GetValue<string>("EmailSettings:Password"));
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
