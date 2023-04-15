using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Domain.Services.Interface;

namespace JobCandidates.Domain.Services.Implementation
{
    public class JobCandidateService : IJobCandidateService
    {
        private readonly IJobCandidateReadRepository _jobCandidateReadRepository;
        private readonly IJobCandidateWriteRepository _jobCandidateWriteRepository;
        private readonly ISkillReadRepository _skillReadRepository;
        public JobCandidateService(IJobCandidateReadRepository jobCandidateReadRepository, IJobCandidateWriteRepository jobCandidateWriteRepository, ISkillReadRepository skillReadRepository)
        {
            _jobCandidateReadRepository = jobCandidateReadRepository;
            _jobCandidateWriteRepository = jobCandidateWriteRepository;
            _skillReadRepository = skillReadRepository;
        }

        public JobCandidate AddNewJobCandidate(JobCandidate jobCandidate, IEnumerable<Guid> skillIds)
        {
            if (_jobCandidateReadRepository.FindJobCandidateByEmail(jobCandidate.Email) != null)
                throw new AlreadyExistsException("Job candidate with given email is already registered");
            IEnumerable<Skill> skills = _skillReadRepository.GetByIds(skillIds);
            jobCandidate.Skills = skills.ToList();
            return _jobCandidateWriteRepository.Add(jobCandidate);
        }

        public void RemoveJobCandidate(Guid jobCandidateId)
        {
            var candidate = _jobCandidateReadRepository.GetById(jobCandidateId);
            if (candidate == null) throw new NotFoundException("Job candidate not found");
            _jobCandidateWriteRepository.Delete(candidate);
        }

        public JobCandidate AddSkillToJobCandidate(Guid jobCandidateId, Guid skillId)
        {
            var candidate = _jobCandidateReadRepository.GetWithSkills(jobCandidateId);
            var skill = _skillReadRepository.GetById(skillId);
            if (candidate == null) throw new NotFoundException("Job candidate not found");
            if (skill == null) throw new NotFoundException("Skill not found");
            if (candidate.Skills.Contains(skill))
                throw new AlreadyExistsException("Job candidate already has that skill");
            candidate.Skills.Add(skill);
            _jobCandidateWriteRepository.Update(candidate);
            return candidate;
        }

        public void RemoveSkillToJobCandidate(Guid jobCandidateId, Guid skillId)
        {
            var candidate = _jobCandidateReadRepository.GetWithSkills(jobCandidateId);
            var skill = _skillReadRepository.GetById(skillId);
            if (candidate == null) throw new NotFoundException("Job candidate not found");
            if (skill == null) throw new NotFoundException("Skill not found");
            if (!candidate.Skills.Contains(skill))
                throw new NotFoundException("Job candidate doesnt not have that skill");
            candidate.Skills.Remove(skill);
            _jobCandidateWriteRepository.Update(candidate);
        }

        public IEnumerable<JobCandidate> SearchCandidates(string fullName, IEnumerable<Guid> skillIds)
        {
            var skills = _skillReadRepository.GetByIds(skillIds);
            return _jobCandidateReadRepository.FindByNameAndSkills(fullName, skills).ToList();
        }
    }
}
