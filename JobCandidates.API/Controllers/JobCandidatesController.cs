using System;
using System.Collections.Generic;
using AutoMapper;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidatesController : ControllerBase
    {
        private readonly IJobCandidateService _jobCandidateService;
        private readonly IMapper _mapper;
        public JobCandidatesController(IJobCandidateService jobCandidateService, IMapper mapper)
        {
            _jobCandidateService = jobCandidateService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetByNameAndSkills([FromQuery] string fullName, [FromQuery] List<Guid> skillIds)
        {
            return Ok(_mapper.Map<IEnumerable<JobCandidateDto>>(_jobCandidateService.SearchCandidates(fullName, skillIds)));
        }

        [HttpPost]
        public IActionResult AddNewJobCandidate(NewJobCandidateDto dto)
        {
            try
            {
                _jobCandidateService.AddNewJobCandidate(_mapper.Map<JobCandidate>(dto), dto.Skills);
            }
            catch(Exception ex)
            {
                switch (ex)
                {
                    case AlreadyExistsException: return BadRequest(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            return Ok("Job candidate added successfully");
        }

        [HttpPost("{id:guid}/skill")]
        public IActionResult AddSkillToJobCandidate(Guid id, EntityIdDto dto)
        {
            try
            {
                _jobCandidateService.AddSkillToJobCandidate(id, dto.Id.Value);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case AlreadyExistsException: return BadRequest(ex.Message);
                    case NotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            return Ok("Skill added to job candidate");
        }

        [HttpDelete("{candidateId:guid}/skill/{skillId:guid}")]
        public IActionResult RemoveSkillFromJobCandidate(Guid candidateId, Guid skillId)
        {
            try
            {
                _jobCandidateService.RemoveSkillToJobCandidate(candidateId, skillId);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case AlreadyExistsException: return BadRequest(ex.Message);
                    case NotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            return Ok("Skill has been deleted from job candidate");
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteJobCandidate(Guid id)
        {
            try
            {
                _jobCandidateService.RemoveJobCandidate(id);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            return Ok("Job candidate deleted");
        }
    }
}
