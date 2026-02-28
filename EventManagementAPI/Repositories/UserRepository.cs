using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//this will be responsible for talking directly to the Db
namespace EventManagementAPI.Repositories
{
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        //precondition: go to the Db
        //postcondition: returns the Users table in a list
        public IEnumerable<User> GetAll() {
            return _context.Users.AsNoTracking().ToList();
        }

        //precondition: go to the Db
        //postcondition: returns one User from the table based on Id
        public User? GetById(Guid Id) {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == Id);
        }

        //precondition: go to the Db
        //postcondition: adds a User to the table (Db)
        public User Add(User us) {
            _context.Users.Add(us);
            _context.SaveChanges();
            return us;
        }

        //precondition:go to the Db
        //postcondition: asynchronous that check if userId is found in the Users Db
        public Task<bool> ExistsAsync(Guid userId) {
            return _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
