namespace EventManagementAPI.Models
{
    //the real model (data) for the Registration
    public class User {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        //one User can have multiple Registrations (one-to-many)
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
