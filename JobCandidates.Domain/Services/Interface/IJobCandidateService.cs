using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Model;

namespace JobCandidates.Domain.Services.Interface
{
    public interface IJobCandidateService
    {
        public JobCandidate AddNewJobCandidate(JobCandidate jobCandidate);
        public void RemoveJobCandidate(Guid jobCandidateId);
        public JobCandidate AddSkillToJobCandidate(Guid jobCandidateId, Guid skillId);
        public void RemoveSkillToJobCandidate(Guid jobCandidateId, Guid skillId);
        public IEnumerable<JobCandidate> SearchCandidate(string fullName, IEnumerable<Guid> skillIds);
    }
}
