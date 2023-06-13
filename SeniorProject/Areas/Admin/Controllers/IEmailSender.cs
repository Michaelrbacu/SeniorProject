using System.Threading.Tasks;

namespace SeniorProject.Areas.Identity.EmailService
{
    public interface IEmailSender
    {
        public bool SendEmail(string email, string subject, string messages);
    }
}
