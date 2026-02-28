using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;

//contains only HTTP requests 
namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController: ControllerBase {
        //call the service that has the methods you need
        private readonly IUserService _userService;
        public UsersController(IUserService userService) {
            _userService = userService;
        }

        //precondition: none
        //postcondition: returns 200 'Ok' which is successful GET
        [HttpGet]
        public ActionResult<List<User>> GetUsers() {
            return Ok(_userService.GetUsers());
        }
        //precondition: service must find an Id
        //postcondition: returns 200 'Ok' with the Id information if successful GET
        [HttpGet("{Id:guid}")]
        public ActionResult<User> GetUser(Guid Id) {
            User? us = _userService.GetUser(Id);
            //if empty then return 400 'Not Found' else return 200 'Ok'
            if(us == null) {
                return NotFound();
            }
            return Ok(us);
        }
        //precondition: service must get the data information and check input validation
        //postcondition: returns the information of the event 
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User input) {
            //firstname, lastname, and email must be filled (if not bad request = invalid input)
            if (string.IsNullOrWhiteSpace(input.FirstName)) {
                return BadRequest("FirstName is required.");
            }
            if (string.IsNullOrWhiteSpace(input.LastName)) {
                return BadRequest("LastName is required.");
            }
            if (string.IsNullOrWhiteSpace(input.Email)) {
                return BadRequest("Email is required.");
            }

            User newUser = _userService.CreateUser(input);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }
    }
}
