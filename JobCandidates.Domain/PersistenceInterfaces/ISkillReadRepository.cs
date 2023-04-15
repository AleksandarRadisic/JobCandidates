using System;
using System.Collections.Generic;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces.Base;

namespace JobCandidates.Domain.PersistenceInterfaces
{
    public interface ISkillReadRepository : IBaseReadRepository<Guid, Skill>
    {
        public Skill FindByName(string name);
        IEnumerable<Skill> GetAll();
        IEnumerable<Skill> GetByIds(IEnumerable<Guid> skillIds);
    }
}
