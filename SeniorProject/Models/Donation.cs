using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Donation
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the donation amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid amount.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter the donation date.")]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }

        // Additional properties
        public string Address { get; set; }
        public int? Phone { get; set; }
        public string Message { get; set; }
    }
}