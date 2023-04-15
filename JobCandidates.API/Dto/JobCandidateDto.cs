using JobCandidates.Domain.Model;
using System.Collections.Generic;
using System;

namespace JobCandidates.API.Dto
{
    public class JobCandidateDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<SkillDto> Skills { get; set; }
    }
}
