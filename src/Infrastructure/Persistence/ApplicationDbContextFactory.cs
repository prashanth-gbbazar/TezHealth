using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TezHealth.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Use your PostgreSQL connection string here
            optionsBuilder.UseNpgsql(
                "Host=187.127.131.13;Port=5432;Database=tez_inventory;Username=tezinventory;Password=WelcomePassword123!"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
