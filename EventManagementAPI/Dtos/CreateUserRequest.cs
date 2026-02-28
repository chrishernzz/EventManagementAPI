//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what the client is allowed to send when creating an user (request only)
    public class CreateUserRequest {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
