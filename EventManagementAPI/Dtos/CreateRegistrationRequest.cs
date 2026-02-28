//Ignore Spelling: Dtos

namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what the client is allowed to send when creating an registration (request only)
    public class CreateRegistrationRequest {
        public Guid UserId { get; set; }
    }
}
