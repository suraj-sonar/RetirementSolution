using AutoFixture;
using Moq;
using FluentAssertions;
using Base.Application.RepositoryContracts;
using Base.Application.Services;
using Microsoft.Extensions.Logging;
using Base.Application.Logging;

namespace Base.Application.test.Services
{
    public class PersonServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPersonRepository> _PersonRepositoryMock;
        private readonly PersonService _personService;
        private readonly Mock<IAppLogger<PersonService>> _loggerMock;
        public PersonServiceTest()
        {
            _fixture = new Fixture();
            _PersonRepositoryMock = _fixture.Freeze<Mock<IPersonRepository>>();
            _loggerMock = _fixture.Freeze<Mock<IAppLogger<PersonService>>>();
            _personService = new PersonService(_PersonRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetGetAllPersonsAsync_ShouldReturnOkResulte_whenDataIsFound()
        {
            var personsMock = _fixture.Create<List<Domain.Person>>();
            _PersonRepositoryMock.Setup(repo => repo.GetAllPersonsAsync()).ReturnsAsync(personsMock);

            var result = _personService.GetAllPersonsAsync().Result;

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(personsMock);
            result.Count.Should().Be(personsMock.Count);
            _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void GetGetAllPersonsAsync_ShouldReturnNull_whenDataIsNotFound()
        {
            List<Domain.Person> personsMock = null;
            _PersonRepositoryMock.Setup(repo => repo.GetAllPersonsAsync()).ReturnsAsync(personsMock);

            var result = _personService.GetAllPersonsAsync().Result;

            result.Should().BeNull();
            _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
            _loggerMock.Verify(logger => logger.LogWarning(It.IsAny<string>()), Times.Once);
        }
    }
}