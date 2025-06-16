using System.Threading.Tasks;
using StudentEventManagementSystem.DTOs;
using StudentEventManagementSystem.Models;

namespace StudentEventManagementSystem.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<(bool Success, string Message, Registration? CreatedRegistration)> RegisterStudentForEventAsync(CreateRegistrationDto registrationDto);
        // You might add methods to get registrations by student or event
    }
}