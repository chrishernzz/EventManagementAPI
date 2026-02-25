namespace EventManagementAPI.Models
{
    //the model (data) for the Event
    public class Event{
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }

        //one Event can have multiple Registrations (one-to-many)
        public ICollection<Registration> Registrations{get;set;} = new List<Registration>();
    }
}
