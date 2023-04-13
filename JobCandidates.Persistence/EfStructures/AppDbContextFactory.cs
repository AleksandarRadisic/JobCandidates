using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JobCandidates.Persistence.EfStructures
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = SetupOptions(args[0]);
            return new AppDbContext(optionsBuilder.Options);
        }


        private static DbContextOptionsBuilder<AppDbContext> SetupOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder;
        }
    }
}
