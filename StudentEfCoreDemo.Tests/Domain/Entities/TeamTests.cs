using StudentEfCoreDemo.Domain.Entities;
using Xunit;

public class TeamTests
{
    [Fact]
    public void Addteam_ShouldAddteamToTeam()
    {
        // Arrange
        var team = new Team { Id = 1, Name = "Team A", SportType = "Basketball", FoundedDate = DateTime.Now, HomeStadium = "Stadium A", Players = new List<Player>() };

        // Act & Assert
        Assert.Equal(1, team.Id);
        Assert.Equal("Team A", team.Name);
        Assert.Equal("Basketball", team.SportType);
        Assert.IsType<DateTime>(team.FoundedDate);
        Assert.Equal("Stadium A", team.HomeStadium);
        Assert.IsAssignableFrom<ICollection<Player>>(team.Players);

    }
}
