using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//the bridge between models and actual SQL Db
namespace EventManagementAPI.Data
{
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //these 3 represent the Db tables
        public DbSet<Event> Events => Set<Event>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Registration> Registrations => Set<Registration>();

        //function will tell EF Core that we want to customize how my tables are created
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //here we are going to edit the Registration Tables
            modelBuilder.Entity<Registration>(entity =>
            {
                //Event 1-many Registrations
                entity.HasOne(r => r.Event).WithMany(e => e.Registrations).HasForeignKey(r => r.EventId).OnDelete(DeleteBehavior.Cascade);

                //User 1-many Registrations
                entity.HasOne(r => r.User).WithMany(e => e.Registrations).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);

                //this will prevent from registering twice for the same event
                entity.HasIndex(r => new { r.EventId, r.UserId }).IsUnique();
            }
            );
        }
    }
}
