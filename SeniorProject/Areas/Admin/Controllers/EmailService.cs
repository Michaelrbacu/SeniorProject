using System.Threading.Tasks;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
            var mandrillApi = new MandrillApi(apiKey);

            var emailMessage = new EmailMessage
            {
                To = new List<EmailAddress> { new EmailAddress { Email = email } },
                FromEmail = "farhanmf1@my.gvltec.edu", // Replace with your own sender email
                FromName = "EarthCare", // Replace with your app or company name
                Subject = subject,
                Html = message
            };

            var request = new SendMessageRequest(emailMessage);
            await mandrillApi.SendMessage(request);
        }
    }
}