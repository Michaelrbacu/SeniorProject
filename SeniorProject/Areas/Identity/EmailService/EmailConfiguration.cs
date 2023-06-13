﻿namespace SeniorProject.Areas.Identity.EmailService
{
    public class EmailConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public bool UseSSL { get; set; }
    }
}
