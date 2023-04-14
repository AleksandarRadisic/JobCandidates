using System;
using AutoMapper;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Domain.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }

        [HttpPut]
        public IActionResult AddSkill(SkillDto dto)
        {
            try
            {
                _skillService.AddSkill(_mapper.Map<Skill>(dto));
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case AlreadyExistsException: return BadRequest(ex.Message);
                    default: return Problem(ex.Message);
                }
            }
            return Ok("Skill added");
        }

        [HttpDelete]
        public IActionResult DeleteSkill(DeleteEntityIdDto dto)
        {
            try
            {
                _skillService.DeleteSkill(dto.Id.Value);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException: return NotFound(ex.Message);
                    default: return Problem(ex.Message);
                }
            }
            return Ok("Skill deleted");
        }
    }
}
