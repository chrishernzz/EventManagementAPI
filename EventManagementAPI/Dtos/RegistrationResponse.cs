//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what data shape that the API chooses to send back to the client (Registrations response only)
    public class RegistrationResponse {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
