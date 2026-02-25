using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services{
    public interface IEventService
    {
        //call all the functions that are going to be used and using IEnumerable to let it be any data type
        IEnumerable<Event> GetEvents();
        Event? GetEvent(Guid Id);
        Event CreateEvent(Event input);
    }
}