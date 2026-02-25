using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IRegistrationService{
        //call all the functions that are going to be used
        Registration CreateRegistration(Guid EventId, Guid UserId);
    }
}
