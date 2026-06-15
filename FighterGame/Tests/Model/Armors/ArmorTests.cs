using FighterGame.Model.Armors;

namespace Tests.Model.Armors;

public class ArmorTests
{
    private const string _noArmorName = "Без брони";
    private const int _noArmorExpectedArmor = 0;

    private const string _firstArmorName = "Лёгкая броня";
    private const int _firstArmorExpectedArmor = 2;

    private const string _secondArmorName = "Средняя броня";
    private const int _secondArmorExpectedArmor = 4;

    private const string _thirdArmorName = "Тяжёлая броня";
    private const int _thirdArmorExpectedArmor = 6;

    public static readonly object[][] ArmorTestData =
    {
        new object[] { new NoArmor(), _noArmorName, _noArmorExpectedArmor },
        new object[] { new FirstArmor(), _firstArmorName, _firstArmorExpectedArmor },
        new object[] { new SecondArmor(), _secondArmorName, _secondArmorExpectedArmor },
        new object[] { new ThirdArmor(), _thirdArmorName, _thirdArmorExpectedArmor }
    };

    [Theory]
    [MemberData( nameof( ArmorTestData ) )]
    public void Constructor_WhenArmorCreated_HasExpectedStats(
        IArmor armor,
        string expectedName,
        int expectedArmor )
    {
        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
        Assert.Equal( expectedName, armor.Name );
    }
}