using AuthSystem.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;


namespace SeniorProject.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public string Id { get; set; } // Add this line

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        // Add any other properties you need for the admin model here

        public List<ApplicationUser> Users { get; set; }


    }
}