using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; } 

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}