using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SeniorProject.Services;

namespace SeniorProject.Areas.Admin.Controllers
{
    public class EmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = false;

                using (var smtpClient = new SmtpClient(_emailSettings.Server, _emailSettings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtpClient.EnableSsl = _emailSettings.UseSSL;
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}