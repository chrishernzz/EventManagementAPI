using EventManagementAPI.Models;
using EventManagementAPI.Repositories;

//cpp - contains business logic of the systems (rules)
namespace EventManagementAPI.Services
{
    //inheritance the IUserService that grabs what the methods SHOULD have
    public class UserService : IUserService{
        //need this to get the data since the bridge is the repository
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        //precondition: none
        //postcondition: returns all the users information
        public List<User> GetUsers(){
            return _userRepository.GetAll();
        }

        //precondition: none
        //postcondition: loops through the users and returns the Id only if found else null
        public User? GetUser(Guid Id){
            return _userRepository.GetById(Id);
        }
        //precondition: none
        //postcondition: returns an user that was created 
        public User CreateUser(User input){
            //create an object of instance here
            User newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = input.FirstName;
            newUser.LastName = input.LastName;
            newUser.Email = input.Email;
            newUser.CreatedAt = input.CreatedAt;

            return _userRepository.Add(newUser);
        }
    }
}
