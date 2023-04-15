using System;
using System.Collections.Generic;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces.Base;

namespace JobCandidates.Domain.PersistenceInterfaces
{
    public interface IJobCandidateReadRepository : IBaseReadRepository<Guid, JobCandidate>
    {
        public JobCandidate FindJobCandidateByEmail(string email);
        JobCandidate GetWithSkills(Guid jobCandidateId);
        IEnumerable<JobCandidate> FindByNameAndSkills(string fullName, IEnumerable<Skill> skills);
    }
}
