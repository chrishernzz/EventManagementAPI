//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what data shape that the API chooses to send back to the client (Registrations response only and includes more details)
    public class RegistrationDetailResponse {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
