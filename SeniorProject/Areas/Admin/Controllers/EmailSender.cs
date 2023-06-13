using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SeniorProject.Services;

namespace SeniorProject.Areas.Admin.Controllers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(_emailSettings.Server, _emailSettings.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    client.EnableSsl = Convert.ToBoolean(_emailSettings.UseSSL);
                    client.Timeout = Convert.ToInt32(_emailSettings.Timeout);

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
                        mailMessage.To.Add(email);
                        mailMessage.Subject = subject;
                        mailMessage.Body = message;
                        mailMessage.IsBodyHtml = true;

                        client.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
