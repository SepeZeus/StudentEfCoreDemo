using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentEfCoreDemo.API.Controllers;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Application.Features.Teams.Queries;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace StudentEfCoreDemo.Tests.API.Controllers
{
    public class TeamsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TeamsController(_mediatorMock.Object);
        }


        [Fact]
        public async Task GetTeamById_ReturnsNotFound_WhenTeamDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTeamByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TeamDto)null);

            // Act
            var result = await _controller.GetTeamById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateTeam_AddsTeamToDatabase()
        {
            // Arrange
            var newTeam = new CreateTeamCommand { Name = "Team A" };
            var createdTeam = new CreateTeamDto { Id = 1, Name = "Team A" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTeamCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdTeam);

            // Act
            var result = await _controller.CreateTeam(newTeam);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var team = Assert.IsType<CreateTeamDto>(createdResult.Value);
            Assert.Equal("Team A", team.Name);
        }

        [Fact]
        public async Task UpdateTeam_UpdatesExistingTeam()
        {
            // Arrange
            var updateTeamCommand = new UpdateTeamCommand { Id = 1, Name = "Updated Team" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTeamCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateTeam(1, updateTeamCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTeam_RemovesTeamFromDatabase()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTeamCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTeam(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}