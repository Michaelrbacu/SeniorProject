using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
