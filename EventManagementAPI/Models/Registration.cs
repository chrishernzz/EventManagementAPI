namespace EventManagementAPI.Models
{
    //the model (data) for the Registration
    public class Registration{
        public Guid Id { get; set; }
        //this is the EVENT.id
        public Guid EventId { get; set; }
        //this is the USER.id
        public Guid UserId { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public Event? Event { get; set; }
        public User? User { get; set; }

    }
}
