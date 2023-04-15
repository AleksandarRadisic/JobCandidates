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
    public class SkillReadRepository : BaseReadRepository<Guid, Skill>, ISkillReadRepository
    {
        public SkillReadRepository(AppDbContext context) : base(context)
        {
        }

        public Skill FindByName(string name)
        {
            return GetSet().FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Skill> GetAll()
        {
            return GetSet().ToList();
        }

        public IEnumerable<Skill> GetByIds(IEnumerable<Guid> skillIds)
        {
            return GetSet().Where(s => skillIds.Contains(s.Id));
        }
    }
}
