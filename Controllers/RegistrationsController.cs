using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Controllers
{
    [Route("api/[controller]")] // api/registrations
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // POST /api/registrations: Register a student for an event.
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(CreateRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, message, createdRegistration) = await _registrationService.RegisterStudentForEventAsync(registrationDto);

            if (!success)
            {
                // Specific error handling based on message
                if (message.Contains("not found"))
                    return NotFound(new { message });
                if (message.Contains("already registered") || message.Contains("past event"))
                    return Conflict(new { message }); // Or BadRequest for past event

                return BadRequest(new { message }); // General error
            }

            // On success, you might want to return the created registration details
            // For now, a 201 Created without a body is fine, or a simple success message
            return StatusCode(201, new { message = "Registration successful.", registrationId = createdRegistration?.Id });
        }
    }
}