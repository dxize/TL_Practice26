using FighterGame.Model.Weapons;

namespace Tests.Model.Weapons;

public class WeaponTests
{
    private const string Sword = "Sword";
    private const int SwordExpectedDamage = 10;

    private const string Spear = "Spear";
    private const int SpearExpectedDamage = 15;

    private const string HandGun = "HandGun";
    private const int HandGunExpectedDamage = 30;

    [Theory]
    [InlineData( Sword, SwordExpectedDamage )]
    [InlineData( Spear, SpearExpectedDamage )]
    [InlineData( HandGun, HandGunExpectedDamage )]
    public void Constructor_WhenWeaponCreated_HasExpectedDamage(
        string weaponName,
        int expectedDamage )
    {
        // Arrange
        IWeapon weapon;

        if ( weaponName == Sword )
        {
            weapon = new SwordWeapon();
        }
        else if ( weaponName == Spear )
        {
            weapon = new SpearWeapon();
        }
        else
        {
            weapon = new HandGunWeapon();
        }

        // Assert
        Assert.Equal( expectedDamage, weapon.Damage );
    }
}