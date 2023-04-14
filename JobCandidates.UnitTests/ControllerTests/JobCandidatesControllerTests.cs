using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobCandidates.API.Controllers;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace JobCandidates.UnitTests.ControllerTests
{
    public class JobCandidatesControllerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IJobCandidateService> _serviceMock = new();

        [Fact]
        public void Add_new_job_candidate_should_return_Ok()
        {
            NewJobCandidateDto dto = new NewJobCandidateDto
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            };
            JobCandidate toService = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555",
            };
            _serviceMock.Setup(s => s.AddNewJobCandidate(toService)).
                Returns(It.IsAny<JobCandidate>());
            _mapperMock.Setup(m => m.Map<JobCandidate>(dto)).Returns(toService);

            JobCandidatesController controller = new JobCandidatesController(_serviceMock.Object, _mapperMock.Object);
            IActionResult result = controller.AddNewJobCandidate(dto);

            result.GetType().ShouldBe(typeof(OkObjectResult));
        }

        [Fact]
        public void Add_new_job_candidate_should_return_Bad_request()
        {
            NewJobCandidateDto dto = new NewJobCandidateDto
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            };
            JobCandidate toService = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555",
            };
            _serviceMock.Setup(s => s.AddNewJobCandidate(toService)).Throws(new AlreadyExistsException(""));
            _mapperMock.Setup(m => m.Map<JobCandidate>(dto)).Returns(toService);

            JobCandidatesController controller = new JobCandidatesController(_serviceMock.Object, _mapperMock.Object);
            IActionResult result = controller.AddNewJobCandidate(dto);

            result.GetType().ShouldBe(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void Add_new_job_candidate_should_return_500()
        {
            NewJobCandidateDto dto = new NewJobCandidateDto
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            };
            JobCandidate toService = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555",
            };
            _serviceMock.Setup(s => s.AddNewJobCandidate(toService)).Throws(new Exception());
            _mapperMock.Setup(m => m.Map<JobCandidate>(dto)).Returns(toService);

            JobCandidatesController controller = new JobCandidatesController(_serviceMock.Object, _mapperMock.Object);
            IActionResult result = controller.AddNewJobCandidate(dto);

            result.GetType().ShouldBe(typeof(ObjectResult));
            ((ObjectResult)result).StatusCode.ShouldBe(500);
        }

        [Fact]
        public void Delete_candidate_should_return_ok()
        {
            DeleteEntityIdDto dto = new DeleteEntityIdDto
            {
                Id = Guid.NewGuid(),
            };

            JobCandidatesController controller = new JobCandidatesController(_serviceMock.Object, _mapperMock.Object);
            var result = controller.DeleteJobCandidate(dto);

            result.GetType().ShouldBe(typeof(OkObjectResult));
        }

        [Fact]
        public void Delete_candidate_should_return_not_found()
        {
            DeleteEntityIdDto dto = new DeleteEntityIdDto
            {
                Id = Guid.NewGuid(),
            };
            _serviceMock.Setup(s => s.RemoveJobCandidate(dto.Id)).Throws(new NotFoundException(""));

            JobCandidatesController controller = new JobCandidatesController(_serviceMock.Object, _mapperMock.Object);
            var result = controller.DeleteJobCandidate(dto);

            result.GetType().ShouldBe(typeof(NotFoundObjectResult));
        }
    }
}
