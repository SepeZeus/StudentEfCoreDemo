using StudentEfCoreDemo.Domain.Entities;
using Xunit;

public class PlayerTests
{
    [Fact]
    public void Player_Should_Have_Valid_Properties()
    {
        // Arrange
        var player = new Player { Id = 1, FirstName = "John", LastName = "Doe", Position = "Guard", TeamId = 1, Goals = 2 };

        // Act & Assert
        Assert.Equal(1, player.Id);
        Assert.Equal("John", player.FirstName);
        Assert.Equal("Doe", player.LastName);
        Assert.Equal("Guard", player.Position);
        Assert.Equal(1, player.TeamId);
        Assert.Equal(2, player.Goals);
    }
}
