using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IUserService{
        //call all the functions that are going to be used
        List<User> GetUsers();
        User? GetUser(Guid Id);
        User CreateUser(User input);
    }
}
