using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SeniorProject.Services; // Import the namespace for EmailSettings

public class EmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using (var client = new SmtpClient(_emailSettings.Server, _emailSettings.Port))
        {
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
            client.EnableSsl = _emailSettings.UseSSL;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}