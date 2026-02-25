using EventManagementAPI.Models;
using EventManagementAPI.Repositories;

//cpp - contains business logic of the systems (rules)
namespace EventManagementAPI.Services
{
    //inheritance the IEventService that grabs what the methods SHOULD have
    public class EventService : IEventService {
        private readonly IEventRepository _eventRepository;

        //constructor
        public EventService(IEventRepository eventRepository) {
            _eventRepository = eventRepository;
        }

        //precondition: none
        //postcondition: returns all the events information from the repository
        public IEnumerable<Event> GetEvents(){
            return _eventRepository.GetAll();
        }

        //precondition: none
        //postcondition: returns the Id that was found from the repository
        public Event? GetEvent(Guid Id){
            return _eventRepository.GetById(Id);
        }

        //precondition: none
        //postcondition: returns an event and adds it to the repository data
        public Event CreateEvent(Event input){
            //create an object of instance here
            Event newEvent = new Event();
            newEvent.Id = Guid.NewGuid();
            newEvent.Title = input.Title;
            newEvent.Description = input.Description;
            newEvent.StartDateTime = input.StartDateTime;
            newEvent.EndDateTime = input.EndDateTime;
            newEvent.Location = input.Location;
            newEvent.Capacity = input.Capacity;

            return _eventRepository.Add(newEvent);
        }
    }
}
