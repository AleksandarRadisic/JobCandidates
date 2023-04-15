using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Persistence.EfStructures;
using JobCandidates.Persistence.Repositories.Base.Implementation;
using Microsoft.EntityFrameworkCore;

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

        public JobCandidate GetWithSkills(Guid jobCandidateId)
        {
            return GetSet().Where(jc => jc.Id == jobCandidateId).Include(jc => jc.Skills).FirstOrDefault();
        }

        public IEnumerable<JobCandidate> FindByNameAndSkills(string fullName, IEnumerable<Skill> skills)
        {
            var nameFilter = FindByName(GetSet().Include(jc => jc.Skills), fullName);
            if (!skills.Any())
               return nameFilter;
            return FindBySkills(nameFilter, skills).ToList();
        }

        private IQueryable<JobCandidate> FindBySkills(IQueryable<JobCandidate> candidates, IEnumerable<Skill> skills)
        {
            return candidates.Where(jc => skills.All(s => jc.Skills.Contains(s)) && jc.Skills.Any());
        }

        private IQueryable<JobCandidate> FindByName(IQueryable<JobCandidate> candidates, string fullName)
        {
            return candidates
                .Include(jc => jc.Skills)
                .Where(jc => jc.FullName.Contains(fullName) || string.IsNullOrWhiteSpace(fullName));
            
        }
    }
}
