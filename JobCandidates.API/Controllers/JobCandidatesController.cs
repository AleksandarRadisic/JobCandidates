using System;
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

        [HttpPut]
        public IActionResult AddNewJobCandidate(NewJobCandidateDto dto)
        {
            try
            {
                _jobCandidateService.AddNewJobCandidate(_mapper.Map<JobCandidate>(dto));
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

        [HttpDelete]
        public IActionResult DeleteJobCandidate([FromBody] DeleteEntityIdDto candidateId)
        {
            try
            {
                _jobCandidateService.RemoveJobCandidate(candidateId.Id.Value);
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
