using MimeKit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Authentication;
using System;
using System.Net.Mail;

namespace SeniorProject.Areas.Identity.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        } 
        public bool SendEmail(string email, string subject, string messages)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.From = new MailAddress(_emailConfig.FromEmail,_emailConfig.FromName);
            mailMessage.Subject = subject;
            mailMessage.Body = messages;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(_emailConfig.Server, _emailConfig.Port);
            smtp.EnableSsl = _emailConfig.UseSSL;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(_emailConfig.Username, _emailConfig.Password);
            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
