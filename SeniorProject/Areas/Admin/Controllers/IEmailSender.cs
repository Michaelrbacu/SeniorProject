﻿using System.Threading.Tasks;

namespace SeniorProject.Areas.Identity.EmailService
{
    public interface IEmailSenderr
    {
        public bool SendEmail(string email, string subject, string messages);
    }
}
