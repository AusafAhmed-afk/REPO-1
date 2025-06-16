using System.ComponentModel.DataAnnotations;

namespace StudentEventManagementSystem.DTOs
{
    public class CreateFeedbackDto
    {
        [Required]
        public int EventId { get; set; }

        // public int StudentId { get; set; } // If you track who submitted

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }
    }
}