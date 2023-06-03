using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }
        public string Email { get; set; }
    }
}
