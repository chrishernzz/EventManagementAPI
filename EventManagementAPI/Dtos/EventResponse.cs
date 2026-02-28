//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is Data Transfer Object that says what data shape that the API chooses to send back to the client (Events response only)
    public class EventResponse {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
