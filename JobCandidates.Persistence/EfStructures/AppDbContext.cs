using JobCandidates.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace JobCandidates.Persistence.EfStructures
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
