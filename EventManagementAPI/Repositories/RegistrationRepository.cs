using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//this will be responsible for talking directly to the Db
namespace EventManagementAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository {
        private readonly ApplicationDbContext _context;

        //constructor
        public RegistrationRepository(ApplicationDbContext context) {
            _context = context;
        }

        //precondition: go to the Db
        //postcondition: adds a Registration to the table (Db)
        public Registration Add(Registration registration) {
            _context.Registrations.Add(registration);
            _context.SaveChanges();
            return registration;
        }
    }
}
