using System;
using System.Collections.Generic;

namespace JobCandidates.Domain.Model
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<JobCandidate> JobCandidates { get; set; }
    }
}
