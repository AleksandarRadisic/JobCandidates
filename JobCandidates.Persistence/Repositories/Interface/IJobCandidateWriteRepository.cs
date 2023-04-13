using Persistence.Repositories.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace JobCandidates.Persistence.Repositories.Interface
{
    public interface IJobCandidateWriteRepository : IBaseWriteRepository<JobCandidate>
    {

    }
}
