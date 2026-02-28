using EventManagementAPI.Models;


namespace EventManagementAPI.Repositories
{
    //header - contains the methods that the system should have and this is repository so only reads data
    public interface IEventRepository {
        IEnumerable<Event> GetAll();
        Event? GetById(Guid id);
        Event Add(Event ev);
        //this indicates that it will return a true or false but might take time since it's going in the Db (asynchronous)
        Task<bool> ExistsAsync(Guid eventId);
    }
}
