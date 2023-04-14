using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Domain.Exceptions;
using JobCandidates.Domain.Model;
using JobCandidates.Domain.PersistenceInterfaces;
using JobCandidates.Domain.Services.Implementation;
using Moq;
using Shouldly;
using Xunit;

namespace JobCandidates.UnitTests.ServiceTests
{
    public class SkillServiceTests
    {
        private readonly Mock<ISkillReadRepository> _readRepoMock = new();
        private readonly Mock<ISkillWriteRepository> _writeRepoMock = new();

        [Fact]
        public void Add_skill_success()
        {
            var skill = new Skill
            {
                Name = "New skill"
            };
            var savedSkill = new Skill
            {
                Name = skill.Name,
                Id = Guid.NewGuid(),
            };
            _readRepoMock.Setup(r => r.FindByName(skill.Name)).Returns((Skill)null);
            _writeRepoMock.Setup(w => w.Add(skill)).Returns(savedSkill);

            var fromService = new SkillService(_readRepoMock.Object, _writeRepoMock.Object).AddSkill(skill);

            _readRepoMock.Verify(r => r.FindByName(skill.Name), Times.Once());
            _writeRepoMock.Verify(w => w.Add(skill), Times.Once());
            fromService.Id.ShouldBe(savedSkill.Id);
        }

        [Fact]
        public void Add_skill_should_throw_exception()
        {
            var skill = new Skill
            {
                Name = "Existing skill",
            };
            var skillFromDb = new Skill
            {
                Name = "Existing skill",
                Id = Guid.NewGuid(),
            };
            _readRepoMock.Setup(r => r.FindByName(skill.Name)).Returns(skillFromDb);

            Assert.Throws<AlreadyExistsException>(() =>
                new SkillService(_readRepoMock.Object, _writeRepoMock.Object).AddSkill(skill));
            _readRepoMock.Verify(r => r.FindByName(skill.Name), Times.Once());
            _writeRepoMock.Verify(w => w.Add(skill), Times.Never());
        }
    }
}
