using StudentEfCoreDemo.Domain.Entities;
using Xunit;

public class PlayerTests
{
    [Fact]
    public void AddPlayer_ShouldAddPlayerToTeam()
    {
        // Arrange
        var team = new Team { Id = 1, Name = "Team A", SportType = "Football", FoundedDate = DateTime.Now, HomeStadium = "Stadium A", Players = new List<Player>() };

        var player = new Player { Id = 1, FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2};


        // Act
        team.Players.Add(player);

        // Assert
        Assert.Contains(player, team.Players);
    }
}
