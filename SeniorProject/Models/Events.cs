using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? EventStart { get; set; }
        public string? EventEnd { get; set; }


    }
}
