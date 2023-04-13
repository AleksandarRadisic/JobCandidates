using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using JobCandidates.Persistence.Repositories.Interface;
using Persistence.EfStructures;
using Persistence.Repositories.Base.Implementation;

namespace JobCandidates.Persistence.Repositories.Implementation
{
    public class SkillReadRepository : BaseReadRepository<Guid, Skill>, ISkillReadRepository
    {
        public SkillReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
