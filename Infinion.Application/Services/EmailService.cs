using Microsoft.Extensions.Configuration;

namespace Infinion.Application.Services;
public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {

    }
}
