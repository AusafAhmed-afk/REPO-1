using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.DTOs
{
    public class CreateEventDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        // You might want a custom validation attribute to ensure date is in the future
        public DateTime Date { get; set; }

        [Required]
        [StringLength(150)]
        public string Venue { get; set; }

        public string? Description { get; set; }
    }
}