using JobCandidates.API.Dto;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillWriteRepository _skillWriteRepository;

        public SkillsController(ISkillWriteRepository skillWriteRepository)
        {
            _skillWriteRepository = skillWriteRepository;
        }

        [HttpPut]
        public IActionResult AddSkill(SkillDto dto)
        {
            Skill skill = new Skill
            {
                Name = dto.Name
            };
            _skillWriteRepository.Add(skill);
            return Ok("Skill added");
        }
    }
}
