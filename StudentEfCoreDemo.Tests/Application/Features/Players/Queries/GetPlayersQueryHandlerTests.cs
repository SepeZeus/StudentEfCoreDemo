using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Players.Queries;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetPlayersQueryHandlerTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock;
    private readonly GetPlayersQueryHandler _handler;

    public GetPlayersQueryHandlerTests()
    {
        _playerRepositoryMock = new Mock<IPlayerRepository>();
        _handler = new GetPlayersQueryHandler(_playerRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPlayers()
    {
        // Arrange
        var players = new List<Player>
        {
            new Player { Id = 1, FirstName = "John", LastName = "Doe" },
            new Player { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };

        _playerRepositoryMock.Setup(repo => repo.GetPlayers()).ReturnsAsync(players);

        // Act
        var result = await _handler.Handle(new GetPlayersQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _playerRepositoryMock.Verify(repo => repo.GetPlayers(), Times.Once);
    }
}

