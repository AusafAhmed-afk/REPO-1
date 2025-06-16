using Microsoft.AspNetCore.Mvc;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Controllers
{
    [Route("api/[controller]")] //  api/events
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET /api/events: List all upcoming events.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetUpcomingEvents()
        {
            var events = await _eventService.GetAllUpcomingEventsAsync();
            return Ok(events);
        }

        // GET /api/events/{id} (Added for completeness, often needed)
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            if (eventDto == null)
            {
                return NotFound(new { message = $"Event with ID {id} not found." });
            }
            return Ok(eventDto);
        }


        // POST /api/events: Create a new event.
        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto createEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEventDto = await _eventService.CreateEventAsync(createEventDto);
            if (newEventDto == null)
            {
                // This case might not happen if CreateEventAsync always creates or throws
                return StatusCode(500, "A problem happened while handling your request.");
            }
            // Return 201 Created status with the location of the new resource and the resource itself
            return CreatedAtAction(nameof(GetEvent), new { id = newEventDto.Id }, newEventDto);
        }

        // PUT /api/events/{id}: Update an event.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventDto updateEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _eventService.UpdateEventAsync(id, updateEventDto);
            if (!success)
            {
                return NotFound(new { message = $"Event with ID {id} not found for update." });
            }
            return NoContent(); // Standard response for successful PUT if no content is returned
        }

        // DELETE /api/events/{id}: Delete an event.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _eventService.DeleteEventAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Event with ID {id} not found for deletion." });
            }
            return NoContent(); // Standard response for successful DELETE
        }

        // GET /api/events/search?query=xyz
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EventDto>>> SearchEvents([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "Search query cannot be empty." });
            }
            var events = await _eventService.SearchEventsAsync(query);
            return Ok(events);
        }

        // GET /api/events/filter?sort=date&venue=Auditorium
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EventDto>>> FilterEvents([FromQuery] string? sort, [FromQuery] string? venue)
        {
            var events = await _eventService.FilterEventsAsync(sort, venue);
            return Ok(events);
        }
    }
}