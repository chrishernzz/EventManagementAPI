using EventManagementAPI.Dtos;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;

//cpp - contains business logic of the systems (rules)
namespace EventManagementAPI.Services
{
    public class RegistrationService : IRegistrationService {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRegistrationRepository _registrationRepository;

        //constructor
        public RegistrationService(IEventRepository eventRepository, IUserRepository userRepository, IRegistrationRepository registrationRepository) {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _registrationRepository = registrationRepository;
        }

        //precondition: must get an Id from /event/id resource URL
        //postcondition: will create an registration based on the event id if found
        public async Task<RegistrationResult> RegisterAsync(Guid eventId, CreateRegistrationRequest request){
            //check if event id exists
            if(!await _eventRepository.ExistsAsync(eventId)) {
                //now call the fail function from Registration Result
                return RegistrationResult.Failure(RegistrationErrorType.EventNotFound, "Event not found.");
            }

            //checks if user id exists
            if(request.UserId == Guid.Empty || !await _userRepository.ExistsAsync(request.UserId)) {
                return RegistrationResult.Failure(RegistrationErrorType.UserNotFound, "User not found.");
            }

            if(await _registrationRepository.ExistsAsync(eventId, request.UserId)) {
                return RegistrationResult.Failure(RegistrationErrorType.DuplicateRegistration,"User is already registered for this event.");
            }
            //create an object of instance of the real model 
            Registration registration = new Registration();
            registration.Id = Guid.NewGuid();
            registration.EventId = eventId;
            registration.UserId = request.UserId;
            registration.RegisteredAt = DateTime.UtcNow;
            registration.Status = "Confirmed";


            //save to the database through the repo since repo can communicate with the Db
            await _registrationRepository.AddAsync(registration);
            await _registrationRepository.SaveChangesAsync();

            //create the response using the DTO
            RegistrationResponse response = new RegistrationResponse();
            response.Id = registration.Id;
            response.EventId = registration.EventId;
            response.UserId = registration.UserId;
            response.RegisteredAt = registration.RegisteredAt;

            return RegistrationResult.Created(response);

        }
    }
}
