using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using EventManagementAPI.Dtos;

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

        //precondition: get information from the DTO 
        //postcondition: returns 200 'Ok' which is successful GET
        [HttpGet]
        public ActionResult<IEnumerable<EventResponse>> GetEvents() {
            return Ok(_eventService.GetEvents());
        }

        //precondition: service must find an Id
        //postcondition: returns 200 'Ok' with the Id information if successful GET
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<EventDetailsResponse>> GetEventById(Guid Id) {
            EventDetailsResponse? response = await _eventService.GetEventByIdAsync(Id);
            if(response == null) {
                return NotFound();
            }
            return Ok(response);
        }

        //precondition:
        //postcondition;
        [HttpGet("{Id:guid}/registrations")]
        public async Task<ActionResult<PagedResult<RegistrationListItemDto>>> GetEventRegistrations(
            Guid id,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] string? status = null,
            [FromQuery] string? search = null) { 

            //conditions statements that check for page and pageSize
            if(page < 1) {
                page = 1;
            }
            if (pageSize < 1) {
                pageSize = 50;
            }
            if (pageSize > 200) {
                pageSize = 200;
            }
            PagedResult<RegistrationListItemDto> result = await _eventService.GetEventRegistrationsAsync(id, page, pageSize, status, search);
            return Ok(result);
        }

        //precondition: call DTO to input certain values that client can request
        //postcondition: returns the information in the Db and gives the 201 response
        [HttpPost]
        public ActionResult<EventResponse> CreateEvent([FromBody] CreateEventRequest input) {
            //must not be empty, title must have some input
            if (string.IsNullOrWhiteSpace(input.Title)) {
                return BadRequest("Title is required");
            }
            //end date must be after start data
            if(input.EndDateTime <= input.StartDateTime) {
                return BadRequest("EndDateTime must be after StartDateTime.");
            }

            EventResponse newEvent = _eventService.CreateEvent(input);
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }
    }
}
