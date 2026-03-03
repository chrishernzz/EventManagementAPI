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
            //go to the Db to get the information
            IEnumerable<Event> eventEnumerable = _eventRepository.GetAll();

            //treating eventEnumerable as a list since in repository we say '.ToList()'
            List<Event>? eventList = eventEnumerable as List<Event>;
            if (eventList == null) {
                //fallback only if GetAll() ever returns something that's not a List
                eventList = new List<Event>(eventEnumerable);
            }
            List<EventResponse> eventResponses = new List<EventResponse>();

            for (int i = 0; i < eventList.Count; i++) {
                Event ev = eventList[i];
                EventResponse response = new EventResponse();
                response.Id = ev.Id;
                response.Title = ev.Title;
                response.Description = ev.Description;
                response.StartDateTime = ev.StartDateTime;
                response.EndDateTime = ev.EndDateTime;
                response.Location = ev.Location;
                response.Capacity = ev.Capacity;

                //add the information from the Db but return the DTO information
                eventResponses.Add(response);
            }
            return eventResponses;
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
            //call the registeredCount in here and subtract it with the total capacity from the real model 
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
            //create an object of instance here of the real model
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
