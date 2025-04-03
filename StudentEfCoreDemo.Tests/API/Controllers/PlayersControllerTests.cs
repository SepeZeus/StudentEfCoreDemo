using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentEfCoreDemo.API.Controllers;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Players.Commands;
using StudentEfCoreDemo.Application.Features.Players.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.API.Controllers
{
    public class PlayersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PlayersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetPlayers_ReturnsEmptyList_WhenNoPlayers()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PlayerDto>());

            // Act
            var result = await _controller.GetPlayers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<PlayerDto>>>(result);
            Assert.Null(actionResult.Value);
        }

        [Fact]
        public async Task GetPlayerById_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPlayerByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PlayerDto)null);

            // Act
            var result = await _controller.GetPlayerById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePlayer_AddsPlayerToDatabase()
        {
            // Arrange
            var newPlayer = new CreatePlayerCommand { FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2 };
            var createdPlayer = new PlayerDto { Id = 1, FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePlayerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdPlayer);

            // Act
            var result = await _controller.CreatePlayer(newPlayer);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var player = Assert.IsType<PlayerDto>(createdResult.Value);
            Assert.Equal("John", player.FirstName);
            Assert.Equal("Doe", player.LastName);
        }

        [Fact]
        public async Task UpdatePlayer_UpdatesExistingPlayer()
        {
            // Arrange
            var updatePlayerCommand = new UpdatePlayerCommand { Id = 1, FirstName = "John", LastName = "Doe", Position = "Attack", TeamId = 1, Goals = 2 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePlayerCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePlayer(1, updatePlayerCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePlayer_RemovesPlayerFromDatabase()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePlayerCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePlayer(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

}