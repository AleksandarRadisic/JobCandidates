using System;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces.Base;

namespace JobCandidates.Domain.PersistenceInterfaces
{
    public interface IJobCandidateReadRepository : IBaseReadRepository<Guid, JobCandidate>
    {
        public JobCandidate FindJobCandidateByEmail(string email);
    }
}
