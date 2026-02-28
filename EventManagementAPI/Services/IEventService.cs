using EventManagementAPI.Dtos;
using EventManagementAPI.Models;

//header - contains the methods that the system should have
namespace EventManagementAPI.Services
{
    public interface IEventService {
        //call all the functions that are going to be used and using IEnumerable to let it be any data type
        IEnumerable<EventResponse> GetEvents();
        //asynchronous with Task that will look for Id then return the whole object
        Task<EventDetailsResponse?> GetEventByIdAsync(Guid id);
        //same logic as above but now this will get a paginated list of registrations for a specific event Id
        Task<PagedResult<RegistrationListItemDto>> GetEventRegistrationsAsync(Guid eventId, int page, int pageSize, string? status, string? search);
        //this is reading the model EventResponse and calling in the request which indicates what inputs the client has to send
        EventResponse CreateEvent(CreateEventRequest request);
    }
}