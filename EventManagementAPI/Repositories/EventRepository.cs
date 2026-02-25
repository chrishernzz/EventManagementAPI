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
        }

        //precondition: go to the Db
        //postcondition: returns all the events information data from the database
        public List<Event> GetAll() {
            return _context.Events.ToList();
        }
        //precondition: go to the Db
        //postcondition: returns one result only from the table that has the 'Id'
        public Event? GetById(Guid id) {
            return _context.Events.Find(id);
        }
        //precondition: go to the Db
        //postcondition: add a Event to the table (Db)
        public Event Add(Event ev){
            _context.Events.Add(ev);
            _context.SaveChanges();
            return ev;
        }
    }
}