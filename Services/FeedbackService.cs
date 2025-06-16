using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
using StudentEventManagementSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, FeedbackResponseDto? CreatedFeedback)> SubmitFeedbackAsync(CreateFeedbackDto feedbackDto)
        {
            var eventItem = await _context.Events.FindAsync(feedbackDto.EventId);
            if (eventItem == null)
            {
                return (false, "Event not found.", null);
            }

            // Ensure feedback is only allowed after the event date
            if (eventItem.Date > DateTime.UtcNow.Date) // Or DateTime.UtcNow if you want to be precise with time
            {
                return (false, "Feedback can only be submitted after the event has occurred.", null);
            }

            // Optional: Check if student was registered for this event before allowing feedback.
            // This would require StudentId in CreateFeedbackDto and checking Registrations table.
            // For now, we'll keep it simple as per minimum requirements.

            var feedback = new Feedback
            {
                EventId = feedbackDto.EventId,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment,
                SubmittedDate = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            var feedbackResponse = new FeedbackResponseDto
            {
                Id = feedback.Id,
                EventId = feedback.EventId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                SubmittedDate = feedback.SubmittedDate
            };

            return (true, "Feedback submitted successfully.", feedbackResponse);
        }
    }
}