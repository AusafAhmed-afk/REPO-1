using System.ComponentModel.DataAnnotations;
namespace StudentEventManagementSystem.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}