using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Queries;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetTeamsQueryHandlerTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock;
    private readonly GetTeamsQueryHandler _handler;

    public GetTeamsQueryHandlerTests()
    {
        _teamRepositoryMock = new Mock<ITeamRepository>();
        _handler = new GetTeamsQueryHandler(_teamRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTeams()
    {
        // Arrange
        var teams = new List<Team>
        {
            new Team { Id = 1, Name = "Team A", SportType = "Football", FoundedDate = DateTime.Now, HomeStadium = "Stadium A", Players = new List<Player>() },
            new Team { Id = 2, Name = "Team B", SportType = "Hockey", FoundedDate = DateTime.Now, HomeStadium = "Stadium B", Players = new List<Player>() },
        };

        _teamRepositoryMock.Setup(repo => repo.GetTeams()).ReturnsAsync(teams);

        // Act
        var result = await _handler.Handle(new GetTeamsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _teamRepositoryMock.Verify(repo => repo.GetTeams(), Times.Once);
    }
}
