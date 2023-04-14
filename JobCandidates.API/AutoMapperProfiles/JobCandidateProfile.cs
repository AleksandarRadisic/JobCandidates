using AutoMapper;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Model;

namespace JobCandidates.API.AutoMapperProfiles
{
    public class JobCandidateProfile : Profile
    {
        public JobCandidateProfile()
        {
            CreateMap<NewJobCandidateDto, JobCandidate>();
        }
    }
}
