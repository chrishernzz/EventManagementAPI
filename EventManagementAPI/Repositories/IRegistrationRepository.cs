using EventManagementAPI.Models;

namespace EventManagementAPI.Repositories
{
    //header - contains the methods that the system should have and this is repository so only reads data
    public interface IRegistrationRepository {
        Registration Add(Registration registration);
    }
}
