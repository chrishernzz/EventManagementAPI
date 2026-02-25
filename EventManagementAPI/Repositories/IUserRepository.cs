using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Repositories
{
    public interface IUserRepository{
        //call all the functions that are going to be used
        List<User> GetAll();
        User? GetById(Guid Id);
        User Add(User us);
    }
}
