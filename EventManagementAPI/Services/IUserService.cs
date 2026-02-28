using EventManagementAPI.Dtos;
using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IUserService {
        //call all the functions that are going to be used
        IEnumerable<UserResponse> GetUsers();
        UserResponse? GetUserById(Guid id);
        UserResponse CreateUser(CreateUserRequest request);
    }
}
