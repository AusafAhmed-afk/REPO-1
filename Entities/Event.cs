using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.Models
{
    public class Event
    {
        public int Id { get; set; } 

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(150)]
        public string Venue { get; set; }

        public string? Description { get; set; } 

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}