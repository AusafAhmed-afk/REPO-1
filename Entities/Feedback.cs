using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEventManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; } 

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; } 


        [Required]
        [Range(1, 5)] 
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
    }
}