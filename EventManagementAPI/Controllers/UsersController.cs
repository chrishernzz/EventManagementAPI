using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
using EventManagementAPI.Dtos;

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
        public ActionResult<IEnumerable<UserResponse>> GetUsers() {
            return Ok(_userService.GetUsers());
        }

        //precondition: service must find an Id
        //postcondition: returns 200 'Ok' with the Id information if successful GET
        [HttpGet("{Id:guid}")]
        public ActionResult<UserResponse> GetUserById(Guid Id) {
            UserResponse? us = _userService.GetUserById(Id);

            //if empty then return 400 'Not Found' else return 200 'Ok'
            if(us == null) {
                return NotFound();
            }
            return Ok(us);
        }
        //precondition: service must get the data information and check input validation
        //postcondition: creates a user and returns the created UserResponse DTO
        [HttpPost]
        public ActionResult<UserResponse> CreateUser([FromBody] CreateUserRequest request) {
            //firstname, lastname, and email must be filled (if not bad request = invalid input)
            if (string.IsNullOrWhiteSpace(request.FirstName)) {
                return BadRequest("FirstName is required.");
            }
            if (string.IsNullOrWhiteSpace(request.LastName)) {
                return BadRequest("LastName is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Email)) {
                return BadRequest("Email is required.");
            }

            UserResponse newUser = _userService.CreateUser(request);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
    }
}
