using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Players.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Application.Features.Players.Commands
{
    public class CreatePlayerCommandHandlerTests
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly Mock<ITeamRepository> _teamRepositoryMock;
        private readonly CreatePlayerCommandHandler _handler;

        public CreatePlayerCommandHandlerTests()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _handler = new CreatePlayerCommandHandler(_playerRepositoryMock.Object, _teamRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreatePlayer()
        {
            // Arrange
            var command = new CreatePlayerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 25,
                Position = "Guard",
                TeamId = 1,
                Goals = 10
            };

            var team = new Team { Id = 1, MaxRosterSize = 25, Players = new List<Player>() };
            _teamRepositoryMock.Setup(repo => repo.GetTeam(1)).ReturnsAsync(team);
            _playerRepositoryMock.Setup(repo => repo.AddPlayer(It.IsAny<Player>())).ReturnsAsync(new Player { Id = 1 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _playerRepositoryMock.Verify(repo => repo.AddPlayer(It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTeamDoesNotExist()
        {
            // Arrange
            var command = new CreatePlayerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 25,
                Position = "Guard",
                TeamId = 1,
                Goals = 10
            };

            _teamRepositoryMock.Setup(repo => repo.GetTeam(1)).ReturnsAsync((Team)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTeamRosterIsFull()
        {
            // Arrange
            var command = new CreatePlayerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 25,
                Position = "Guard",
                TeamId = 1,
                Goals = 10
            };

            var team = new Team { Id = 1, MaxRosterSize = 1, Players = new List<Player> { new Player() } };
            _teamRepositoryMock.Setup(repo => repo.GetTeam(1)).ReturnsAsync(team);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }


}