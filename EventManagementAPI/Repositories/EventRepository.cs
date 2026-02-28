using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//this will be responsible for talking directly to the Db
namespace EventManagementAPI.Repositories
{
    //cpp - contains business logic of the systems (rules) but only contains access to the data
    public class EventRepository : IEventRepository {
        private readonly ApplicationDbContext _context;

        //constructor
        public EventRepository(ApplicationDbContext context) {
            _context = context;
            //create an empty list of integers
            List<int> dsd = new List<int>();
        }

        //precondition: go to the Db
        //postcondition: returns all the Events information data from the database
        public IEnumerable<Event> GetAll() {
            return _context.Events.AsNoTracking().ToList();
        }

        //precondition: go to the Db
        //postcondition: returns one result only from the table that has the 'Id'
        public Event? GetById(Guid id) {
            //loop through the Event then set e to e.Id and compare with the parameter
            return _context.Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        //precondition: go to the Db
        //postcondition: add a Event to the table (Db)
        public Event Add(Event ev){
            _context.Events.Add(ev);
            _context.SaveChanges();
            return ev;
        }

        //precondition:go to the Db
        //postcondition: asynchronous that check if eventId is found in the Events Db
        public Task<bool> ExistsAsync(Guid eventId) {
            return _context.Events.AnyAsync(e => e.Id == eventId);
        }
    }
}