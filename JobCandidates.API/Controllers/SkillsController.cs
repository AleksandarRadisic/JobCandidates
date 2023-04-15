using System;
using System.Collections.Generic;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(_skillService.GetAll()));
        }

        [HttpPost]
        public IActionResult AddSkill(NewSkillDto dto)
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

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteSkill(Guid id)
        {
            try
            {
                _skillService.DeleteSkill(id);
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
