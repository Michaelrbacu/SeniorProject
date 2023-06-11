using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string? EventDescription { get; set; }
        public string? Registered { get; set; }

    }
}