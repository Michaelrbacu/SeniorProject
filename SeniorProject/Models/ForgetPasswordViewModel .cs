using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}