using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EfStructures
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
