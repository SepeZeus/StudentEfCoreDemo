using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Queries;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetTeamByIdQueryHandlerTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock;
    private readonly GetTeamByIdQueryHandler _handler;

    public GetTeamByIdQueryHandlerTests()
    {
        _teamRepositoryMock = new Mock<ITeamRepository>();
        _handler = new GetTeamByIdQueryHandler(_teamRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTeam_WhenTeamExists()
    {
        // Arrange
        var team = new Team { Id = 1, Name = "Team A", SportType = "Football", FoundedDate = DateTime.Now, HomeStadium = "Stadium A", Players = new List<Player>() };
        _teamRepositoryMock.Setup(repo => repo.GetTeam(1)).ReturnsAsync(team);

        // Act
        var result = await _handler.Handle(new GetTeamByIdQuery(1), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Team A", result.Name);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenTeamDoesNotExist()
    {
        // Arrange
        _teamRepositoryMock.Setup(repo => repo.GetTeam(1)).ReturnsAsync((Team)null);

        // Act
        var result = await _handler.Handle(new GetTeamByIdQuery(1), CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}

