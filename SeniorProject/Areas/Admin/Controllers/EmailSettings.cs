﻿namespace SeniorProject.Services
{
    public class EmailSettings
    {

        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public int Timeout { get; set; } // Add this property

        public bool UseSSL { get; set; }
    }
}