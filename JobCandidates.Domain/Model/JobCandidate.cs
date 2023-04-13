using System;
using System.Collections.Generic;

namespace JobCandidates.Domain.Model
{
    public class JobCandidate
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}
