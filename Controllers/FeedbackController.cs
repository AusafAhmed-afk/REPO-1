using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Controllers
{
    [Route("api/[controller]")] // api/feedback
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // POST /api/feedback: Submit feedback for an event
        [HttpPost]
        public async Task<ActionResult<FeedbackResponseDto>> SubmitFeedback(CreateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, message, createdFeedback) = await _feedbackService.SubmitFeedbackAsync(feedbackDto);

            if (!success)
            {
                // Specific error handling
                if (message.Contains("not found"))
                    return NotFound(new { message });
                if (message.Contains("after the event"))
                    return BadRequest(new { message }); // Or a more specific status like 403 Forbidden

                return BadRequest(new { message }); // General error
            }

            // Return 201 Created with the feedback response
            return StatusCode(201, createdFeedback);
        }
    }
}