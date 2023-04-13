using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces.Base;

namespace JobCandidates.Domain.PersistenceInterfaces
{
    public interface ISkillWriteRepository : IBaseWriteRepository<Skill>
    {
    }
}
