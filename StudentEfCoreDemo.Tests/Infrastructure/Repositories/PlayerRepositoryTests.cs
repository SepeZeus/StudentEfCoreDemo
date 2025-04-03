using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using StudentEfCoreDemo.Infrastructure.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Infrastructure.Repositories
{
    public class PlayerRepositoryTests
    {
        private StudentContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: "PlayerTestDb")
                .Options;
            return new StudentContext(options);
        }

        [Fact]
        public async Task AddPlayer_ShouldAddPlayerToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new PlayerRepository(context);
            var player = new Player { FirstName = "John", LastName = "Doe" };

            // Act
            await repository.AddPlayer(player);

            // Assert
            var savedPlayer = await context.Players.FirstOrDefaultAsync(p => p.FirstName == "John" && p.LastName == "Doe");
            Assert.NotNull(savedPlayer);
        }

        [Fact]
        public async Task GetPlayer_ShouldReturnPlayer_WhenPlayerExists()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new PlayerRepository(context);
            var player = new Player { FirstName = "John", LastName = "Doe" };
            context.Players.Add(player);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetPlayer(player.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(player.Id, result.Id);
        }

        [Fact]
        public async Task GetPlayer_ShouldReturnNull_WhenPlayerDoesNotExist()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new PlayerRepository(context);

            // Act
            var result = await repository.GetPlayer(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePlayer_ShouldUpdatePlayerInDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new PlayerRepository(context);
            var player = new Player { FirstName = "John", LastName = "Doe" };
            context.Players.Add(player);
            await context.SaveChangesAsync();

            player.FirstName = "Updated";
            player.LastName = "Name";

            // Act
            await repository.UpdatePlayer(player);

            // Assert
            var updatedPlayer = await context.Players.FirstOrDefaultAsync(p => p.Id == player.Id);
            Assert.Equal("Updated", updatedPlayer.FirstName);
            Assert.Equal("Name", updatedPlayer.LastName);
        }

        [Fact]
        public async Task DeletePlayer_ShouldRemovePlayerFromDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new PlayerRepository(context);
            var player = new Player { FirstName = "John", LastName = "Doe" };
            context.Players.Add(player);
            await context.SaveChangesAsync();

            // Act
            await repository.DeletePlayer(player.Id);

            // Assert
            var deletedPlayer = await context.Players.FirstOrDefaultAsync(p => p.Id == player.Id);
            Assert.Null(deletedPlayer);
        }
    }


}