using EventManagementAPI.Dtos;
using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IRegistrationService {
        //creating a task that indicates it will return a value no matter how long it takes
        Task<RegistrationResult> RegisterAsync(Guid eventId, CreateRegistrationRequest request);
    }
}
