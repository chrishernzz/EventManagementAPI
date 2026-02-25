using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;

//contains only HTTP requests 
namespace EventManagementAPI.Controllers
{
    [ApiController]
    //this name will come from the EventsController and does not include Controller just 'Events'
    [Route("api/[controller]")]
    public class EventsController: ControllerBase{
        //must follow the interface systems rules
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService) {
            //set the interface (methods) to the business logic
            _eventService = eventService;
        }

        //precondition: none
        //postcondition: returns 200 'Ok' which is successful GET
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents() {
            return Ok(_eventService.GetEvents());
        }
        //precondition: service must find an Id
        //postcondition: returns 200 'Ok' with the Id information if successful GET
        [HttpGet("{Id:guid}")]
        public ActionResult<Event> GetEvent(Guid Id) {
            Event? ev = _eventService.GetEvent(Id);
            if(ev == null) {
                return NotFound();
            }
            return Ok(ev);
        }
        //precondition: service must get the data information
        //postcondition: returns the information of the event 
        [HttpPost]
        public ActionResult<Event> CreateEvent([FromBody] Event input) {
            //must not be empty, title must have some input
            if (string.IsNullOrWhiteSpace(input.Title)) {
                return BadRequest("Title is required");
            }
            if(input.EndDateTime <= input.StartDateTime) {
                return BadRequest("EndDateTime must be after StartDateTime.");
            }
            Event newEvent = _eventService.CreateEvent(input);
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
        }
    }
}
