using AutoMapper;
using JobCandidates.Domain.Services.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.API.Controllers;
using JobCandidates.API.Dto;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace JobCandidates.UnitTests.ControllerTests
{
    public class SkillControllerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ISkillService> _serviceMock = new();
        [Fact]
        public void Add_new_skill_should_return_ok()
        {
            var dto = new NewSkillDto
            {
                Name = "New skill"
            };
            var skill = new Skill
            {
                Name = dto.Name,
            };
            var savedSkill = new Skill
            {
                Name = skill.Name,
                Id = Guid.NewGuid()
            };
            _mapperMock.Setup(m => m.Map<Skill>(dto)).Returns(skill);
            _serviceMock.Setup(s => s.AddSkill(skill)).Returns(skill);

            var result = new SkillsController(_serviceMock.Object, _mapperMock.Object).AddSkill(dto);

            result.GetType().ShouldBe(typeof(OkObjectResult));
        }

        [Fact]
        public void Add_new_skill_should_return_bad_request()
        {
            var dto = new NewSkillDto
            {
                Name = "Existing skill"
            };
            var skill = new Skill
            {
                Name = dto.Name,
            };
            _mapperMock.Setup(m => m.Map<Skill>(dto)).Returns(skill);
            _serviceMock.Setup(s => s.AddSkill(skill)).Throws(new AlreadyExistsException(" "));
            
            var result = new SkillsController(_serviceMock.Object, _mapperMock.Object).AddSkill(dto);

            result.GetType().ShouldBe(typeof(BadRequestObjectResult));

        }

        [Fact]
        public void Delete_skill_should_return_ok()
        {
            var dto = new EntityIdDto
            {
                Id = Guid.NewGuid()
            };

            var result = new SkillsController(_serviceMock.Object, _mapperMock.Object).DeleteSkill(dto.Id.Value);

            _serviceMock.Verify(s => s.DeleteSkill(dto.Id.Value), Times.Once());
            result.GetType().ShouldBe(typeof(OkObjectResult));
        }
    }
}
