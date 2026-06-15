using FighterGame.Model.Classes;

namespace Tests.Model.Classes;

public class ClassTests
{
    private const string _knight = "Рыцарь";
    private const int _knightExpectedDamage = 8;
    private const int _knightExpectedHealth = 5;
    private const int _knightExpectedArmor = 5;

    private const string _peasant = "Крестьянин";
    private const int _peasantExpectedDamage = 6;
    private const int _peasantExpectedHealth = 2;
    private const int _peasantExpectedArmor = 2;

    private const string _wolf = "Волк";
    private const int _wolfExpectedDamage = 10;
    private const int _wolfExpectedHealth = 6;
    private const int _wolfExpectedArmor = 1;

    public static readonly TheoryData<IClass, string, int, int, int> ClassTestData = new()
    {
        { new KnightClass(), _knight, _knightExpectedDamage, _knightExpectedHealth, _knightExpectedArmor },
        { new PeasantClass(), _peasant, _peasantExpectedDamage, _peasantExpectedHealth, _peasantExpectedArmor },
        { new WolfClass(), _wolf, _wolfExpectedDamage, _wolfExpectedHealth, _wolfExpectedArmor }
    };

    [Theory]
    [MemberData( nameof( ClassTestData ) )]
    public void Constructor_WhenClassCreated_HasExpectedStats(
        IClass classType,
        string expectedName,
        int expectedDamage,
        int expectedHealth,
        int expectedArmor )
    {
        // Assert
        Assert.Equal( expectedDamage, classType.Damage );
        Assert.Equal( expectedHealth, classType.Health );
        Assert.Equal( expectedArmor, classType.Armor );
        Assert.Equal( expectedName, classType.Name );
    }
}