using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace LMS.Web.Services;

public class EmailSender(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromAddress = configuration["EmailSettings:DefaultEmailAddress"];
        var smtpServer = configuration["EmailSettings:Server"];
        var smtpPort = Convert.ToInt32(configuration["EmailSettings:Port"]);

        MailMessage message = new()
        {
            From = new MailAddress(fromAddress!),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(email));

        using var client = new SmtpClient(smtpServer, smtpPort);

        await client.SendMailAsync(message);
    }
}
