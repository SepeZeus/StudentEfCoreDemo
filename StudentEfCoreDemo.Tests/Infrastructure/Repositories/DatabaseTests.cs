using Microsoft.EntityFrameworkCore;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using System.Threading.Tasks;
using Xunit;

namespace StudentEfCoreDemo.Tests.Infrastructure.Repositories
{
    public class DatabaseTests
    {
        private readonly DbContextOptions<SportsContext> _dbContextOptions;

        public DatabaseTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<SportsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddPlayer_ShouldAddPlayerToDatabase()
        {
            // Arrange
            using var context = new SportsContext(_dbContextOptions);
            var player = new Player { Id = 1, FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2 };

            // Act
            context.Players.Add(player);
            await context.SaveChangesAsync();

            // Assert
            var savedPlayer = await context.Players.FirstOrDefaultAsync(p => p.FirstName == "John" && p.LastName == "Doe");
            Assert.NotNull(savedPlayer);
        }
    }
}