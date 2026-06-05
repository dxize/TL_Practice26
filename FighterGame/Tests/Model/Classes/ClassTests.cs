using FighterGame.Model.Classes;

namespace Tests.Model.Classes;

public class ClassTests
{
    [Fact]
    public void Constructor_WhenKnightClassCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 8;
        int expectedHealth = 5;
        int expectedArmor = 5;

        // Act
        KnightClass classType = new();

        // Assert
        Assert.Equal( expectedDamage, classType.Damage );
        Assert.Equal( expectedHealth, classType.Health );
        Assert.Equal( expectedArmor, classType.Armor );
    }

    [Fact]
    public void Constructor_WhenPeasantClassCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 6;
        int expectedHealth = 2;
        int expectedArmor = 2;

        // Act
        PeasantClass classType = new();

        // Assert
        Assert.Equal( expectedDamage, classType.Damage );
        Assert.Equal( expectedHealth, classType.Health );
        Assert.Equal( expectedArmor, classType.Armor );
    }

    [Fact]
    public void Constructor_WhenWolfClassCreated_HasExpectedStats()
    {
        // Arrange
        int expectedDamage = 10;
        int expectedHealth = 6;
        int expectedArmor = 1;

        // Act
        WolfClass classType = new();

        // Assert
        Assert.Equal( expectedDamage, classType.Damage );
        Assert.Equal( expectedHealth, classType.Health );
        Assert.Equal( expectedArmor, classType.Armor );
    }
}