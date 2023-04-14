using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Domain.Services.Interface;

namespace JobCandidates.Domain.Services.Implementation
{
    public class SkillService : ISkillService
    {
        private readonly ISkillReadRepository _skillReadRepository;
        private readonly ISkillWriteRepository _skillWriteRepository;
        public SkillService(ISkillReadRepository skillReadRepository, ISkillWriteRepository skillWriteRepository)
        {
            _skillReadRepository = skillReadRepository;
            _skillWriteRepository = skillWriteRepository;
        }

        public Skill AddSkill(Skill skill)
        {
            if (_skillReadRepository.FindByName(skill.Name) != null)
                throw new AlreadyExistsException("Skill already exists");
            return _skillWriteRepository.Add(skill);
        }

        public void DeleteSkill(Guid skillId)
        {
            Skill skill = _skillReadRepository.GetById(skillId);
            if (skill == null) throw new NotFoundException("Skill not found");
            _skillWriteRepository.Delete(skill);
        }
    }
}
