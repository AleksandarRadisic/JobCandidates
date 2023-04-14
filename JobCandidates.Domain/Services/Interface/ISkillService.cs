using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Model;

namespace JobCandidates.Domain.Services.Interface
{
    public interface ISkillService
    {
        public Skill AddSkill(Skill skill);
        public void DeleteSkill(Guid skillId);
    }
}
