using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using StudentEfCoreDemo.Infrastructure.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Infrastructure.Repositories
{
    public class TeamRepositoryTests
    {
        private SportsContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SportsContext>()
                .UseInMemoryDatabase(databaseName: "TeamRepositoryTestDb")
                .Options;
            return new SportsContext(options);
        }

        [Fact]
        public async Task AddTeam_ShouldAddTeamToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new TeamRepository(context);
            var team = new Team
            {
                Name = "Team A",
                SportType = "Football",
                FoundedDate = DateTime.Now,
                HomeStadium = "Stadium A",
                MaxRosterSize = 25,
                Players = new List<Player>()
            };

            // Act
            await repository.AddTeam(team);

            // Assert
            var savedTeam = await context.Teams.FirstOrDefaultAsync(t => t.Name == "Team A");
            Assert.NotNull(savedTeam);
        }
    }
}