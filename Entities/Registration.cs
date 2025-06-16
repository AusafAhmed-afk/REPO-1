using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEventManagementSystem.Models
{
    public class Registration
    {
        public int Id { get; set; } 

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; } 

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; } 

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}