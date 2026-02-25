using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventManagementAPI.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>{
        public ApplicationDbContext CreateDbContext(string[] args){
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=eventmanagement.db");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
