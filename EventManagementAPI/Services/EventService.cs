using EventManagementAPI.Dtos;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;

//cpp - contains business logic of the systems (rules)
namespace EventManagementAPI.Services
{
    //inheritance the IEventService that grabs what the methods SHOULD have
    public class EventService : IEventService {
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;

        //constructor
        public EventService(IEventRepository eventRepository, IRegistrationRepository registrationRepository) {
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }

        //precondition: none
        //postcondition: returns all the events information from the repository
        public IEnumerable<EventResponse> GetEvents(){
            //converting input type Event to output type EventResponse since not all data from Event is needed
            return _eventRepository.GetAll().Select(ev => new EventResponse { 
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                StartDateTime = ev.StartDateTime,
                EndDateTime = ev.EndDateTime,
                Location = ev.Location,
                Capacity = ev.Capacity
            });
        }

        //precondition: call the Db from the registration to count how many are registered
        //postcondition: returns a DTO that includes Event details information
        public async Task<EventDetailsResponse?> GetEventByIdAsync(Guid Id){
            Event? ev = _eventRepository.GetById(Id);
            //if ev is empty then return
            if(ev == null) {
                return null;
            }

            //get the count of how many times someone register
            int registeredCount = await _registrationRepository.CountByEventIdAsync(Id);
            //call the registeredCount in here and subtract it with the total capacity
            int remainingCapacity = Math.Max(0, ev.Capacity - registeredCount);

            EventDetailsResponse response = new EventDetailsResponse();
            response.Id = ev.Id;
            response.Title = ev.Title;
            response.Description = ev.Description;
            response.StartDateTime = ev.StartDateTime;
            response.EndDateTime = ev.EndDateTime;
            response.Location = ev.Location;
            response.Capacity = ev.Capacity;
            response.RegisteredCount = registeredCount;
            response.RemainingCapacity = remainingCapacity;

            return response;
        }

        //precondition: none
        //postcondition: return a page information details based on the Id
        public Task<PagedResult<RegistrationListItemDto>> GetEventRegistrationsAsync(Guid eventId, int page, int pageSize, string? status, string? search) {
            return _registrationRepository.GetPagedByEventIdAsync(eventId, page, pageSize, status, search);
        }

        //precondition: call the Event to get the information of the Db
        //postcondition: returns an DTO that does not include all the values from the Event
        public EventResponse CreateEvent(CreateEventRequest request){
            //create an object of instance here
            Event ev = new Event();
            ev.Id = Guid.NewGuid();
            ev.Title = request.Title;
            ev.Description = request.Description;
            ev.StartDateTime = request.StartDateTime;
            ev.EndDateTime = request.EndDateTime;
            ev.Location = request.Location;
            ev.Capacity = request.Capacity;

            //add the event to the Db
            Event created = _eventRepository.Add(ev);

            //now going to convert the entity (values) to the DTO
            EventResponse response = new EventResponse();
            response.Id = created.Id;
            response.Title = created.Title;
            response.Description = created.Description;
            response.StartDateTime = created.StartDateTime;
            response.EndDateTime = created.EndDateTime;
            response.Location = created.Location;
            response.Capacity = created.Capacity;

            return response;
        }
    }
}
