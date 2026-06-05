using FighterGame.Model.Weapons;

namespace Tests.Model.Weapons;

public class WeaponTests
{
    [Fact]
    public void Constructor_WhenSwordWeaponCreated_HasExpectedDamage()
    {
        // Arrange
        int expectedDamage = 10;

        // Act
        SwordWeapon weapon = new();

        // Assert
        Assert.Equal( expectedDamage, weapon.Damage );
    }

    [Fact]
    public void Constructor_WhenSpearWeaponCreated_HasExpectedDamage()
    {
        // Arrange
        int expectedDamage = 15;

        // Act
        SpearWeapon weapon = new();

        // Assert
        Assert.Equal( expectedDamage, weapon.Damage );
    }

    [Fact]
    public void Constructor_WhenHandGunWeaponCreated_HasExpectedDamage()
    {
        // Arrange
        int expectedDamage = 30;

        // Act
        HandGunWeapon weapon = new();

        // Assert
        Assert.Equal( expectedDamage, weapon.Damage );
    }
}