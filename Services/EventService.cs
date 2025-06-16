using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
using StudentEventManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDto>> GetAllUpcomingEventsAsync()
        {
            // Only show events that are today or in the future
            return await _context.Events
                                 .Where(e => e.Date >= DateTime.UtcNow.Date)
                                 .OrderBy(e => e.Date)
                                 .Select(e => new EventDto
                                 {
                                     Id = e.Id,
                                     Name = e.Name,
                                     Date = e.Date,
                                     Venue = e.Venue,
                                     Description = e.Description
                                 }).ToListAsync();
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return null;
            return new EventDto
            {
                Id = ev.Id,
                Name = ev.Name,
                Date = ev.Date,
                Venue = ev.Venue,
                Description = ev.Description
            };
        }

        public async Task<EventDto?> CreateEventAsync(CreateEventDto createEventDto)
        {
            var newEvent = new Event
            {
                Name = createEventDto.Name,
                Date = createEventDto.Date,
                Venue = createEventDto.Venue,
                Description = createEventDto.Description
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return new EventDto // Return DTO
            {
                Id = newEvent.Id,
                Name = newEvent.Name,
                Date = newEvent.Date,
                Venue = newEvent.Venue,
                Description = newEvent.Description
            };
        }

        public async Task<bool> UpdateEventAsync(int id, UpdateEventDto updateEventDto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            ev.Name = updateEventDto.Name;
            ev.Date = updateEventDto.Date;
            ev.Venue = updateEventDto.Venue;
            ev.Description = updateEventDto.Description;

            _context.Entry(ev).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            // Consider what to do with registrations or feedbacks associated with this event
            // For now, let's just delete the event. EF Core might cascade delete if configured, or fail if there are FK constraints.
            // For explicit control, you might load related entities and remove them first, or configure cascade delete in OnModelCreating.

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventDto>> SearchEventsAsync(string query)
        {
            query = query.ToLower();
            return await _context.Events
                                 .Where(e => e.Name.ToLower().Contains(query) || e.Venue.ToLower().Contains(query))
                                 .Where(e => e.Date >= DateTime.UtcNow.Date) // Show only upcoming
                                 .OrderBy(e => e.Date)
                                 .Select(e => new EventDto
                                 {
                                     Id = e.Id,
                                     Name = e.Name,
                                     Date = e.Date,
                                     Venue = e.Venue,
                                     Description = e.Description
                                 }).ToListAsync();
        }

        public async Task<IEnumerable<EventDto>> FilterEventsAsync(string? sortBy, string? filterVenue)
        {
            var queryableEvents = _context.Events
                                        .Where(e => e.Date >= DateTime.UtcNow.Date) // Show only upcoming
                                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterVenue))
            {
                queryableEvents = queryableEvents.Where(e => e.Venue.ToLower().Contains(filterVenue.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "date":
                        queryableEvents = queryableEvents.OrderBy(e => e.Date);
                        break;
                    case "venue":
                        queryableEvents = queryableEvents.OrderBy(e => e.Venue);
                        break;
                    default:
                        queryableEvents = queryableEvents.OrderBy(e => e.Date); // Default sort
                        break;
                }
            }
            else
            {
                queryableEvents = queryableEvents.OrderBy(e => e.Date); // Default sort if no sortBy specified
            }

            return await queryableEvents.Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date,
                Venue = e.Venue,
                Description = e.Description
            }).ToListAsync();
        }
    }
}