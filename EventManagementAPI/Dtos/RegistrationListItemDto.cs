//Ignore Spelling: Dtos, Dto
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what data shape that the API chooses to send back to the client (Registrations response only)
    public class RegistrationListItemDto {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
