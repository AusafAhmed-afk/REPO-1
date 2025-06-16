using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.DTOs
{
    public class CreateRegistrationDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}