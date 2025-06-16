using Microsoft.EntityFrameworkCore;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;
using StudentEventManagementSystem.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEventManagementSystem.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, Registration? CreatedRegistration)> RegisterStudentForEventAsync(CreateRegistrationDto registrationDto)
        {
            // Check if student exists
            var studentExists = await _context.Students.AnyAsync(s => s.Id == registrationDto.StudentId);
            if (!studentExists)
            {
                return (false, "Student not found.", null);
            }

            // Check if event exists and is in the future
            var eventItem = await _context.Events.FindAsync(registrationDto.EventId);
            if (eventItem == null)
            {
                return (false, "Event not found.", null);
            }
            if (eventItem.Date <= DateTime.UtcNow)
            {
                return (false, "Cannot register for a past event.", null);
            }


            // Check for duplicate registration (using the unique index we configured in OnModelCreating)
            var existingRegistration = await _context.Registrations
                .AnyAsync(r => r.StudentId == registrationDto.StudentId && r.EventId == registrationDto.EventId);

            if (existingRegistration)
            {
                return (false, "Student is already registered for this event.", null);
            }

            var registration = new Registration
            {
                StudentId = registrationDto.StudentId,
                EventId = registrationDto.EventId,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Registrations.Add(registration);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Successfully registered.", registration);
            }
            catch (DbUpdateException ex) // Catch potential unique constraint violation just in case
            {
                // Log ex
                return (false, "An error occurred during registration. It's possible the student is already registered.", null);
            }
        }
    }
}