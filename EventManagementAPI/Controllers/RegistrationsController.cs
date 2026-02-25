using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;

//contains only HTTP requests 
namespace EventManagementAPI.Controllers
{
    [ApiController]
    //this route will give me the POST /events{id}/registrations
    [Route("api/events/{Id:guid}/registrations")]
    public class RegistrationsController : ControllerBase{
        //call both methods since it will be needed
        private readonly IEventService _eventService;
        private readonly IRegistrationService _registrationService;

        //constructor
        public RegistrationsController(IEventService eventService, IRegistrationService registrationService){
            _eventService = eventService;
            _registrationService = registrationService;
        }


        //precondition: check if event Id was found
        //postcondition: returns an registration of the event that was created
        [HttpPost]
        public ActionResult<Registration> CreateRegistration(Guid Id, [FromBody] Guid UserId) {
            //have to look for the event Id then check if it exists if yes then create an Registrations
            Event? ev = _eventService.GetEvent(Id);
            if(ev == null) {
                return NotFound();
            }
            //pass in the 
            Registration reg = _registrationService.CreateRegistration(Id, UserId);
            return Created("", reg);
        }

    }
}
