using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Mandrill.API.Endpoints;
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



        public async Task SendEmailAsync(string email, string subject, string messages)
        {
            try
            {

                MailAddress from = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
                MailAddress to = new MailAddress(email);
                MailMessage message = new MailMessage(from, to);
                message.Body = messages;
                message.Subject = subject;
                message.IsBodyHtml = false;
                string server = _emailSettings.Server;
                SmtpClient client = new SmtpClient(server);
                client.UseDefaultCredentials = Convert.ToBoolean("false");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = Convert.ToInt16(_emailSettings.Port);
                client.EnableSsl = Convert.ToBoolean(_emailSettings.UseSSL);
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.Timeout = Convert.ToInt16(9000);
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}