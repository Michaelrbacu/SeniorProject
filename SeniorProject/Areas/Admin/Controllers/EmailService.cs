using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmationEmailAsync(string email, string subject, string message)
        {
            var apiKey = _configuration["Mailchimp:ApiKey"];
            var smtpUsername = _configuration["Mailchimp:SmtpUsername"];
            var fromEmail = _configuration["Mailchimp:FromEmail"];

            // Configure the SMTP client
            using (var smtpClient = new SmtpClient("smtp.mandrillapp.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, apiKey);
                smtpClient.EnableSsl = true;

                // Create the email message
                var fromAddress = new MailAddress(fromEmail, "EarthCare");
                var to = new MailAddress(email);
                var mailMessage = new MailMessage(fromAddress, to)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                // Send the email
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}