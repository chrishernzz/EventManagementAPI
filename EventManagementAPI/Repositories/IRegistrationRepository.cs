using EventManagementAPI.Models;

namespace EventManagementAPI.Repositories
{
    public interface IRegistrationRepository{
        Registration Add(Registration registration);
    }
}
