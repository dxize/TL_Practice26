using FighterGame.Model.Weapons;

namespace Tests.Model.Weapons;

public class WeaponTests
{
    private const string _sword = "Меч";
    private const int _swordExpectedDamage = 10;

    private const string _spear = "Копьё";
    private const int _spearExpectedDamage = 15;

    private const string _handGun = "Пистолет";
    private const int _handGunExpectedDamage = 30;

    public static readonly TheoryData<IWeapon, string, int> WeaponTestData = new()
    {
        { new SwordWeapon(), _sword, _swordExpectedDamage },
        { new SpearWeapon(), _spear, _spearExpectedDamage },
        { new HandGunWeapon(), _handGun, _handGunExpectedDamage }
    };

    [Theory]
    [MemberData( nameof( WeaponTestData ) )]
    public void Constructor_WhenWeaponCreated_HasExpectedStats(
        IWeapon weapon,
        string expectedName,
        int expectedDamage )
    {
        // Assert
        Assert.Equal( expectedDamage, weapon.Damage );
        Assert.Equal( expectedName, weapon.Name );
    }
}