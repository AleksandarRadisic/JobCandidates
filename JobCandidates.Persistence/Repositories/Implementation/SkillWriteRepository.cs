using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Persistence.EfStructures;
using JobCandidates.Persistence.Repositories.Base.Implementation;

namespace JobCandidates.Persistence.Repositories.Implementation
{
    public class SkillWriteRepository : BaseWriteRepository<Skill>, ISkillWriteRepository
    {
        public SkillWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
