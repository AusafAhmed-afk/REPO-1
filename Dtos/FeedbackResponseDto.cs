namespace StudentEventManagementSystem.DTOs
{
    public class FeedbackResponseDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        // public int StudentId { get; set; } // If relevant
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}