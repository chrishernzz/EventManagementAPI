using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//this will be responsible for talking directly to the Db
namespace EventManagementAPI.Repositories
{
    //cpp - contains business logic of the systems (rules) but only contains access to the data
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        //constructor
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //precondition: none
        //postcondition: returns all the events information data from the database
        public List<Event> GetAll()
        {
            return _context.Events.ToList();
        }
        //precondition:
        //postcondition:
        public Event? GetById(Guid id)
        {
            return _context.Events.Find(id);
        }
        //precondition:
        //postcondition:
        public Event Add(Event ev)
        {
            _context.Events.Add(ev);
            _context.SaveChanges();
            return ev;
        }
    }
}