using EventManagementAPI.Dtos;
using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IRegistrationService {
        Task<RegistrationResult> RegisterAsync(Guid eventId, CreateRegistrationRequest request);
    }
}
