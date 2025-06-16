using StudentEventManagementSystem.DTOs;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<(bool Success, string Message, FeedbackResponseDto? CreatedFeedback)> SubmitFeedbackAsync(CreateFeedbackDto feedbackDto);
        // You might add methods to get feedback for an event
    }
}