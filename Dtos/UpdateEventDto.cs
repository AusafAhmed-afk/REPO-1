using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.DTOs
{
    public class UpdateEventDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(150)]
        public string Venue { get; set; }

        public string? Description { get; set; }
    }
}