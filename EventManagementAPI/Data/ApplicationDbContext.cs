using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//the bridge between models and actual SQL Db
namespace EventManagementAPI.Data
{
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //these 3 represent the Db tables
        public DbSet<Event> Events => Set<Event>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Registration> Registrations => Set<Registration>();
    }
}
