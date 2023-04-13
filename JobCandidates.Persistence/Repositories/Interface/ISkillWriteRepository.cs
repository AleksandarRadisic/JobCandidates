using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Persistence.Repositories.Base.Interface;

namespace JobCandidates.Persistence.Repositories.Interface
{
    public interface ISkillWriteRepository : IBaseWriteRepository<Skill>
    {
    }
}
