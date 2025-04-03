using Moq;
using StudentEfCoreDemo.Application.Features.Players.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Application.Features.Players.Commands
{
    public class DeletePlayerCommandHandlerTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly DeletePlayerCommandHandler _handler;

        public DeletePlayerCommandHandlerTests()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _handler = new DeletePlayerCommandHandler(_playerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeletePlayer()
        {
            // Arrange
            var command = new DeletePlayerCommand(1);
            _playerRepositoryMock.Setup(repo => repo.DeletePlayer(1)).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _playerRepositoryMock.Verify(repo => repo.DeletePlayer(1), Times.Once);
        }
    }

}