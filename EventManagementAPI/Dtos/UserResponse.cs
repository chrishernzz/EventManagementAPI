//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{

    //this is Data Transfer Object that says what data shape that the API chooses to send back to the client (Users response only)
    public class UserResponse {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}