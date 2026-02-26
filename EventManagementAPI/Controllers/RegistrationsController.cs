using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;

//contains only HTTP requests 
namespace EventManagementAPI.Controllers
{
    [ApiController]
    //this route will give me the POST /events{id}/registrations
    [Route("api/events/{eventId:guid}/registrations")]
    public class RegistrationsController : ControllerBase {
        //call both methods since it will be needed
        private readonly IEventService _eventService;
        private readonly IRegistrationService _registrationService;

        //constructor
        public RegistrationsController(IEventService eventService, IRegistrationService registrationService) {
            _eventService = eventService;
            _registrationService = registrationService;
        }


        //precondition: check if event Id was found
        //postcondition: returns an registration of the event that was created
        [HttpPost]
        public ActionResult<Registration> CreateRegistration([FromRoute] Guid eventId, [FromBody] Guid UserId) {
            //have to look for the event Id then check if it exists if yes then create an Registrations
            Event? ev = _eventService.GetEvent(eventId);
            if(ev == null) {
                return NotFound();
            }
            //pass in the 
            Registration reg = _registrationService.CreateRegistration(eventId, UserId);
            //return Created("", reg);

            //option 1 you can do
            /*Registration dto = new RegistrationDto(
                reg.Id,
                reg.EventId,
                reg.UserId,
                reg.RegisteredAt,
                reg.Status
             );

            return Created("",dto);
            */

            //otpion 2 where you just call what you want to pass
            return Created("", new { reg.Id });
        }

    }
}
//DTO using record that says it shuold have that type of data
//public record RegistrationDto(
//    Guid Id,
//    Guid EventId,
//    Guid UserId,
//    DateTime RegisteredAt,
//    string Status
//);