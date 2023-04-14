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
    public class JobCandidateServiceTests
    {
        private readonly Mock<IJobCandidateReadRepository> _readRepoMock = new();
        private readonly Mock<IJobCandidateWriteRepository> _writeRepoMock = new();
        [Fact]
        public void New_candidate_should_return_new_candidate()
        {
            var newJobCandidate = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            };
            _readRepoMock.
                Setup(r => r.FindJobCandidateByEmail(newJobCandidate.Email)).
                Returns((JobCandidate)null);
            var savedCandidate = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555",
                Id = Guid.NewGuid()
            };
            _writeRepoMock.
                Setup(w => w.Add(newJobCandidate)).
                Returns(savedCandidate);

            var fromRepo = new JobCandidateService(_readRepoMock.Object, _writeRepoMock.Object).
                AddNewJobCandidate(newJobCandidate);

            fromRepo.Id.ShouldNotBe(Guid.Empty);
            _readRepoMock.
                Verify(r => r.FindJobCandidateByEmail(newJobCandidate.Email), Times.Once());
            _writeRepoMock.
                Verify(wr => wr.Add(newJobCandidate), Times.Once());
        }

        [Fact]
        public void New_candidate_should_throw_exception()
        {
            var newJobCandidate = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            };
            var fromRepoCandidate = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Perica Kojic",
                PhoneNumber = "063234678"
            };
            _readRepoMock.Setup(r => r.FindJobCandidateByEmail(newJobCandidate.Email)).Returns(fromRepoCandidate);
            var service =
                new JobCandidateService(_readRepoMock.Object, _writeRepoMock.Object);
            Assert.Throws<AlreadyExistsException>(() => service.AddNewJobCandidate(newJobCandidate));
            _readRepoMock.
                Verify(r => r.FindJobCandidateByEmail(newJobCandidate.Email), Times.Once());
            _writeRepoMock.
                Verify(wr => wr.Add(newJobCandidate), Times.Never());

        }

        [Fact]
        public void Delete_candidate_success()
        {
            var toBeDeleted = new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555",
                Id = Guid.NewGuid()
            };
            _readRepoMock.Setup(r => r.GetById(toBeDeleted.Id)).Returns(toBeDeleted);

            new JobCandidateService(_readRepoMock.Object, _writeRepoMock.Object).RemoveJobCandidate(toBeDeleted.Id);

            _readRepoMock.Verify(r => r.GetById(toBeDeleted.Id), Times.Once());
            _writeRepoMock.Verify(w => w.Delete(toBeDeleted), Times.Once());
        }

        [Fact]
        public void Delete_candidate_should_throw_exception()
        {
            Guid guid = Guid.NewGuid();
            _readRepoMock.Setup(r => r.GetById(guid)).Returns((JobCandidate)null);
            
            Assert.Throws<NotFoundException>(() => new JobCandidateService(_readRepoMock.Object, _writeRepoMock.Object).RemoveJobCandidate(guid));
            _readRepoMock.Verify(r => r.GetById(guid), Times.Once());
            _writeRepoMock.Verify(w => w.Delete(It.IsAny<JobCandidate>()), Times.Never());
        }
    }
    /*List<JobCandidate> candidates = new List<JobCandidate>();
            candidates.Add(new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "kojicpetar@gmail.com",
                FullName = "Petar Kojic",
                PhoneNumber = "064777555"
            });
            candidates.Add(new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "momcilodj@gmail.com",
                FullName = "Momcilo Djurdjevic",
                PhoneNumber = "064111222"
            });
            candidates.Add(new JobCandidate
            {
                DateOfBirth = new DateTime(1980, 2, 10),
                Email = "pvanja@gmail.com",
                FullName = "Vanja Petrovic",
                PhoneNumber = "064222333"
            });*/
}
