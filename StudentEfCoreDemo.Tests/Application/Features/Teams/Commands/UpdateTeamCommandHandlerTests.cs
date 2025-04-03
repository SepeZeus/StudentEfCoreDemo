using Moq;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Application.Features.Teams.Commands
{
    public class UpdateTeamCommandHandlerTests
    {
        private readonly Mock<ITeamRepository> _teamRepositoryMock;
        private readonly UpdateTeamCommandHandler _handler;

        public UpdateTeamCommandHandlerTests()
        {
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _handler = new UpdateTeamCommandHandler(_teamRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateTeam()
        {
            // Arrange
            var command = new UpdateTeamCommand
            {
                Id = 1,
                Name = "Team A",
                SportType = "Football",
                FoundedDate = DateTime.Now,
                HomeStadium = "Stadium A",
                MaxRosterSize = 25,
                Players = new List<PlayerDto>()
            };

            _teamRepositoryMock.Setup(repo => repo.UpdateTeam(It.IsAny<Team>())).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _teamRepositoryMock.Verify(repo => repo.UpdateTeam(It.IsAny<Team>()), Times.Once);
        }
    }
}