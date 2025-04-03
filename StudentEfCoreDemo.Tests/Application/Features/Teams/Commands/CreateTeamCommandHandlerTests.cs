using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class CreateTeamCommandHandlerTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock;
    private readonly CreateTeamCommandHandler _handler;

    public CreateTeamCommandHandlerTests()
    {
        _teamRepositoryMock = new Mock<ITeamRepository>();
        _handler = new CreateTeamCommandHandler(_teamRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateTeam()
    {
        // Arrange
        var command = new CreateTeamCommand
        {
            Name = "Team A",
            SportType = "Football",
            FoundedDate = DateTime.Now,
            HomeStadium = "Stadium A",
            MaxRosterSize = 25,
            Players = new List<PlayerDto>()
        };

        _teamRepositoryMock.Setup(repo => repo.AddTeam(It.IsAny<Team>())).ReturnsAsync(new Team { Id = 1 });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _teamRepositoryMock.Verify(repo => repo.AddTeam(It.IsAny<Team>()), Times.Once);
    }
}
