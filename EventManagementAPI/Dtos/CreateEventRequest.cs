//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what the client is allowed to send when creating an event (request only)
    public class CreateEventRequest {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}