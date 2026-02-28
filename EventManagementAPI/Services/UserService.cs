using EventManagementAPI.Dtos;
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
        public  IEnumerable<UserResponse> GetUsers() {
            return _userRepository.GetAll().Select(u => new UserResponse{
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            });
        }

        //precondition: none
        //postcondition: returns UserResponse if user exists, otherwise null
        public UserResponse? GetUserById(Guid Id) {
            User? user = _userRepository.GetById(Id);
            //check if id found if not then return
            if(user == null) {
                return null;
            }

            UserResponse response = new UserResponse();
            response.Id = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.CreatedAt = user.CreatedAt;

            return response;
        }

        //precondition: call the User to get the information of the Db
        //postcondition: returns an DTO that does not include all the values from the User
        public UserResponse CreateUser(CreateUserRequest request) {
            //create an object of instance here with the setters
            User newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.Email = request.Email;
            newUser.CreatedAt = DateTime.UtcNow;

            //this goes to repository and inserts to the Db and comes back when done
            User created = _userRepository.Add(newUser);

            //now going to convert the entity (values) to the DTO
            UserResponse response = new UserResponse();
            response.Id = created.Id;
            response.FirstName = created.FirstName;
            response.LastName = created.LastName;
            response.Email = created.Email;
            response.CreatedAt = created.CreatedAt;

            return response;
        }
    }
}
