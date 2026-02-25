using EventManagementAPI.Models;
using EventManagementAPI.Repositories;

//cpp - contains business logic of the systems (rules)
namespace EventManagementAPI.Services
{
    public class RegistrationService : IRegistrationService {
        private readonly IRegistrationRepository _registrationRepository;

        //constructor
        public RegistrationService(IRegistrationRepository registrationRepository) {
            _registrationRepository = registrationRepository;
        }

        //precondition: must get an Id from /event/id resource URL
        //postcondition: will create an registration based on the event id if found
        public Registration CreateRegistration(Guid EventId, Guid UserId){
            Registration newRegistration = new Registration();
            newRegistration.Id = Guid.NewGuid();
            newRegistration.EventId = EventId;
            newRegistration.UserId = UserId;
            newRegistration.RegisteredAt = DateTime.UtcNow;
            newRegistration.Status = "Registered";

            return _registrationRepository.Add(newRegistration);
        }
    }
}
