
using System.ComponentModel.DataAnnotations;

namespace EmailDatabase.Models
{
    public class Email
    {
        public int EmailId { get; set; }

        [Required(ErrorMessage = "You must enter in an email address.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You must enter in a password.")]
        public string Password { get; set; }
    }
}
