using Moq;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class DeleteTeamCommandHandlerTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock;
    private readonly DeleteTeamCommandHandler _handler;

    public DeleteTeamCommandHandlerTests()
    {
        _teamRepositoryMock = new Mock<ITeamRepository>();
        _handler = new DeleteTeamCommandHandler(_teamRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteTeam()
    {
        // Arrange
        var command = new DeleteTeamCommand(1);
        _teamRepositoryMock.Setup(repo => repo.DeleteTeam(1)).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _teamRepositoryMock.Verify(repo => repo.DeleteTeam(1), Times.Once);
    }
}

