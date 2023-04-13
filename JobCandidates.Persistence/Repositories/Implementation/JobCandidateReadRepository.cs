using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Persistence.EfStructures;
using JobCandidates.Persistence.Repositories.Base.Implementation;

namespace JobCandidates.Persistence.Repositories.Implementation
{
    public class JobCandidateReadRepository : BaseReadRepository<Guid, JobCandidate>, IJobCandidateReadRepository
    {
        public JobCandidateReadRepository(AppDbContext context) : base(context)
        {
        }

        public JobCandidate FindJobCandidateByEmail(string email)
        {
            return GetSet().FirstOrDefault(x => x.Email == email);
        }
    }
}
