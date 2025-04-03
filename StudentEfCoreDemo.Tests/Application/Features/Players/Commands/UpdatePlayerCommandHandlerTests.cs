using Moq;
using StudentEfCoreDemo.Application.Features.Players.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class UpdatePlayerCommandHandlerTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock;
    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerCommandHandlerTests()
    {
        _playerRepositoryMock = new Mock<IPlayerRepository>();
        _handler = new UpdatePlayerCommandHandler(_playerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdatePlayer()
    {
        // Arrange
        var player = new Player { Id = 1, FirstName = "John", LastName = "Doe" };
        var command = new UpdatePlayerCommand
        {
            Id = 1,
            FirstName = "Updated",
            LastName = "Name",
            Age = 30,
            Position = "Forward",
            TeamId = 1,
            Goals = 15
        };

        _playerRepositoryMock.Setup(repo => repo.GetPlayer(1)).ReturnsAsync(player);
        _playerRepositoryMock.Setup(repo => repo.UpdatePlayer(It.IsAny<Player>())).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _playerRepositoryMock.Verify(repo => repo.UpdatePlayer(It.IsAny<Player>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPlayerDoesNotExist()
    {
        // Arrange
        var command = new UpdatePlayerCommand
        {
            Id = 1,
            FirstName = "Updated",
            LastName = "Name",
            Age = 30,
            Position = "Forward",
            TeamId = 1,
            Goals = 15
        };

        _playerRepositoryMock.Setup(repo => repo.GetPlayer(1)).ReturnsAsync((Player)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}


