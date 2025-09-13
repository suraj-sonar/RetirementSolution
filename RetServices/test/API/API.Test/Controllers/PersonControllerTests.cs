using AutoFixture;
using Moq;
using FluentAssertions;
using Base.Application.ServiceContracts;
using Base.Api.Controllers;
using Microsoft.Extensions.Logging;
using Base.Application.Logging;
using Base.Domain;
using Microsoft.AspNetCore.Mvc;



namespace API.Tests.Controllers;

public class PersonControllerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IPersonService> _personServiceMock;
    private readonly PersonController _controller;
    private readonly Mock<IAppLogger<PersonController>> _logger;

    public PersonControllerTests()
    {
        _fixture = new Fixture();
        _personServiceMock = _fixture.Freeze<Mock<IPersonService>>();
        _logger = _fixture.Freeze < Mock < IAppLogger < PersonController >>> ();
        _controller = new PersonController(_personServiceMock.Object, _logger.Object);
    }
    [Fact]
    public async Task Get_ShouldReturnOkResponse_WhenDataFound()
    {
        // Arrange
        var personsMock=_fixture.Create<List<Person>>();
        _personServiceMock.Setup(service => service.GetAllPersonsAsync()).ReturnsAsync(personsMock);
        // Act
        var resulte= await _controller.Get().ConfigureAwait(false);
        // Assert
        //Assert.NotNull(resulte);
        resulte.Should().NotBeNull();
        
        //check that service retun http 200 code and data
        var okResult = resulte as OkObjectResult;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(personsMock);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenDataNotFound()
    {
        // Arrange
        List<Person> personsMock = null;
        _personServiceMock.Setup(service => service.GetAllPersonsAsync()).ReturnsAsync(personsMock);
        // Act
        var resulte = await _controller.Get().ConfigureAwait(false);
        // Assert
        var Result = resulte as NotFoundObjectResult;
        Result.StatusCode.Should().Be(404);
        Result.Value.Should().BeEquivalentTo(personsMock);

    }

    [Fact]
    public async Task Get_ShouldLogInformation_WhenCalled()
    {
        // Arrange
        var persons = new List<Person> { new Person() };
        _personServiceMock.Setup(s => s.GetAllPersonsAsync()).ReturnsAsync(persons);

        // Act
        await _controller.Get();

        // Assert
        _logger.Verify(
            x => x.LogInformation("Getting all persons"),
            Times.Once);
    }

    // #4: Get_ShouldReturnInternalServerError_OnException
    [Fact]
    public async Task Get_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        _personServiceMock.Setup(s => s.GetAllPersonsAsync()).ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _controller.Get();

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetByID_ShouldReturnBadRequest_WhenIdIsInvalid()
    {
        // Arrange
        int invalidId = 0;

        // Act
        var result = await _controller.Get(invalidId);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().Be("ID must be greater than zero.");
    }

    [Fact]
    public async Task GetByID_ShouldReturnOkResponse_WhenPersonFound()
    {
        // Arrange
        var personMock = _fixture.Create<Person>();
        _personServiceMock.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>())).ReturnsAsync(personMock);
        // Act
        var resulte = await _controller.Get(1).ConfigureAwait(false);
        
        // Assert
        resulte.Should().NotBeNull();
        //check that service retun http 200 code and data
        var okResult = resulte as OkObjectResult;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(personMock);
    }
    public async Task GetByID_ShouldReturnNotFound_WhenPersonNotFound()
    {
        // Arrange
        Person personMock = null;
        _personServiceMock.Setup(service => service.GetPersonByIdAsync(It.IsAny<int>())).ReturnsAsync(personMock);
        // Act
        var resulte = await _controller.Get(1).ConfigureAwait(false);
        // Assert
        var Result = resulte as NotFoundObjectResult;
        Result.StatusCode.Should().Be(404);
        Result.Value.Should().BeEquivalentTo(personMock);

    }

    [Fact]
    public async Task GetByID_ShouldLogInformation_WhenCalled()
    {
        // Arrange
        var person = new Person();
        _personServiceMock.Setup(s => s.GetPersonByIdAsync(It.IsAny<int>())).ReturnsAsync(person);
        int testId = 1;
        // Act
        await _controller.Get(testId);

        // Assert
        _logger.Verify(
                       x => x.LogInformation($"Getting person with ID: {testId}"),
                                  Times.Once);
    }
}
