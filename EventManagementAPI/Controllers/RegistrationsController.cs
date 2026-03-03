using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using EventManagementAPI.Dtos;

//contains only HTTP requests 
namespace EventManagementAPI.Controllers
{
    [ApiController]
    //this route will give me the POST /events{id}/registrations
    [Route("api/events/{eventId:guid}/registrations")]
    public class RegistrationsController : ControllerBase {
        private readonly IRegistrationService _registrationService;

        //constructor
        public RegistrationsController(IRegistrationService registrationService) {
            _registrationService = registrationService;
        }


        //precondition: check if event Id was found
        //postcondition: returns an registration of the event that was created
        [HttpPost]
        public async Task<ActionResult<RegistrationResponse>> RegisterUserForEvent(Guid eventId, [FromBody] CreateRegistrationRequest request) {
            RegistrationResult result = await _registrationService.RegisterAsync(eventId, request);

            if(result.Registration == null) {
                ErrorResponse error = new ErrorResponse(result.ErrorMessage ?? "Invalid request.");
                if (result.ErrorType == RegistrationErrorType.EventNotFound) {
                    return NotFound(error);
                }
                else if (result.ErrorType == RegistrationErrorType.UserNotFound) {
                    return NotFound(error);
                }
                else if (result.ErrorType == RegistrationErrorType.DuplicateRegistration) {
                    return Conflict(error);
                }
                else {
                    return BadRequest(error);
                }
            }

            //success that it worked
            string location = "/api/events/" + result.Registration.EventId.ToString() + "/registrations/" + result.Registration.Id.ToString();
            return Created(location,result.Registration);
        }
    }
}