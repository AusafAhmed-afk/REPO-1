using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models; // For Event model if needed for complex return
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllUpcomingEventsAsync();
        Task<EventDto?> GetEventByIdAsync(int id);
        Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto);
        Task<bool> UpdateEventAsync(int id, UpdateEventDto updateEventDto);
        Task<bool> DeleteEventAsync(int id);
        Task<IEnumerable<EventDto>> SearchEventsAsync(string query);
        Task<IEnumerable<EventDto>> FilterEventsAsync(string? sortBy, string? filterVenue); // Added filterVenue
    }
}