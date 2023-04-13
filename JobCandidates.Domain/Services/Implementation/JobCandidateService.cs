using System;
using System.Collections.Generic;
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
        public JobCandidateService(IJobCandidateReadRepository jobCandidateReadRepository, IJobCandidateWriteRepository jobCandidateWriteRepository)
        {
            _jobCandidateReadRepository = jobCandidateReadRepository;
            _jobCandidateWriteRepository = jobCandidateWriteRepository;
        }

        public JobCandidate AddNewJobCandidate(JobCandidate jobCandidate)
        {
            if (_jobCandidateReadRepository.FindJobCandidateByEmail(jobCandidate.Email) == null)
                throw new AlreadyExistsException("Job candidate with given email is already registered");
            return _jobCandidateWriteRepository.Add(jobCandidate);
        }

        public void RemoveJobCandidate(Guid jobCandidateId)
        {
            throw new NotImplementedException();
        }

        public JobCandidate AddSkillToJobCandidate(Guid jobCandidateId, Guid skillId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSkillToJobCandidate(Guid jobCandidateId, Guid skillId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobCandidate> SearchCandidate(string fullName, IEnumerable<Guid> skillIds)
        {
            throw new NotImplementedException();
        }
    }
}
