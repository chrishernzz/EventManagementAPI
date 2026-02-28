using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Repositories
{
    public interface IUserRepository {
        //call all the functions that are going to be used
        IEnumerable<User> GetAll();
        User? GetById(Guid id);
        User Add(User user);
        Task<bool> ExistsAsync(Guid userId);
    }
}
