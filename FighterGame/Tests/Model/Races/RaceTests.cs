using FighterGame.Model.Races;

namespace Tests.Model.Races;

public class RaceTests
{
    [Fact]
    public void Constructor_WhenAsianRaceCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 8;
        int expectedHealth = 70;
        int expectedArmor = 10;

        // Act
        AsianRace race = new();

        // Assert
        Assert.Equal( expectedDamage, race.Damage );
        Assert.Equal( expectedHealth, race.Health );
        Assert.Equal( expectedArmor, race.Armor );
    }

    [Fact]
    public void Constructor_WhenEuropeanRaceCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 10;
        int expectedHealth = 90;
        int expectedArmor = 4;

        // Act
        EuropeanRace race = new();

        // Assert
        Assert.Equal( expectedDamage, race.Damage );
        Assert.Equal( expectedHealth, race.Health );
        Assert.Equal( expectedArmor, race.Armor );
    }

    [Fact]
    public void Constructor_WhenNegroidRaceCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 12;
        int expectedHealth = 110;
        int expectedArmor = 1;

        // Act
        NegroidRace race = new();

        // Assert
        Assert.Equal( expectedDamage, race.Damage );
        Assert.Equal( expectedHealth, race.Health );
        Assert.Equal( expectedArmor, race.Armor );
    }
}