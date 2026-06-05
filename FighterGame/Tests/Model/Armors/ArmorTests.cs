using FighterGame.Model.Armors;

namespace Tests.Model.Armors;

public class ArmorTests
{
    [Fact]
    public void Constructor_WhenNoArmorCreated_HasNoArmor()
    {
        // Arrange
        int expectedArmor = 0;

        // Act
        NoArmor armor = new();

        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
    }

    [Fact]
    public void Constructor_WhenFirstArmorCreated_HasExpectedArmor()
    {
        // Arrange
        int expectedArmor = 2;

        // Act
        FirstArmor armor = new();

        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
    }

    [Fact]
    public void Constructor_WhenSecondArmorCreated_HasExpectedArmor()
    {
        // Arrange
        int expectedArmor = 4;

        // Act
        SecondArmor armor = new();

        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
    }

    [Fact]
    public void Constructor_WhenThirdArmorCreated_HasExpectedArmor()
    {
        // Arrange
        int expectedArmor = 6;

        // Act
        ThirdArmor armor = new();

        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
    }
}