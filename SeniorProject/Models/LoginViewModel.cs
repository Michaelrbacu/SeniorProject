using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public IList<AuthenticationScheme> ExternalLoginProviders { get; set; }
    }
}