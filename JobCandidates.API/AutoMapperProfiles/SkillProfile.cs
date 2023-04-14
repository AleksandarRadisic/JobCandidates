using AutoMapper;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Model;

namespace JobCandidates.API.AutoMapperProfiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillDto, Skill>();
        }
    }
}
