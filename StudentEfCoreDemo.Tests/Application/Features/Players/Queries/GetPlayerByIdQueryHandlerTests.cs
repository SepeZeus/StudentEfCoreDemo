using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Players.Queries;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Application.Features.Players.Queries
{
    public class GetPlayerByIdQueryHandlerTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly GetPlayerByIdQueryHandler _handler;

        public GetPlayerByIdQueryHandlerTests()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _handler = new GetPlayerByIdQueryHandler(_playerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPlayer_WhenPlayerExists()
        {
            // Arrange
            var player = new Player { Id = 1, FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2 };
            _playerRepositoryMock.Setup(repo => repo.GetPlayer(1)).ReturnsAsync(player);

            // Act
            var result = await _handler.Handle(new GetPlayerByIdQuery(1), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("Guard", result.Position);
            Assert.Equal(1, result.TeamId);
            Assert.Equal(2, result.Goals);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenPlayerDoesNotExist()
        {
            // Arrange
            _playerRepositoryMock.Setup(repo => repo.GetPlayer(1)).ReturnsAsync((Player)null);

            // Act
            var result = await _handler.Handle(new GetPlayerByIdQuery(1), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }

}