using FighterGame.Model.Races;

namespace Tests.Model.Races;

public class RaceTests
{
    private const string _asian = "Азиат";
    private const int _asianExpectedDamage = 8;
    private const int _asianExpectedHealth = 70;
    private const int _asianExpectedArmor = 10;

    private const string _european = "Европеец";
    private const int _europeanExpectedDamage = 10;
    private const int _europeanExpectedHealth = 90;
    private const int _europeanExpectedArmor = 4;

    private const string _negroid = "Негроид";
    private const int _negroidExpectedDamage = 12;
    private const int _negroidExpectedHealth = 110;
    private const int _negroidExpectedArmor = 1;

    public static readonly object[][] RaceTestData =
    {
        new object[] { new AsianRace(), _asian, _asianExpectedDamage, _asianExpectedHealth, _asianExpectedArmor },
        new object[] { new EuropeanRace(), _european, _europeanExpectedDamage, _europeanExpectedHealth, _europeanExpectedArmor },
        new object[] { new NegroidRace(), _negroid, _negroidExpectedDamage, _negroidExpectedHealth, _negroidExpectedArmor }
    };

    [Theory]
    [MemberData( nameof( RaceTestData ) )]
    public void Constructor_WhenRaceCreated_HasExpectedStats(
        IRace race,
        string expectedName,
        int expectedDamage,
        int expectedHealth,
        int expectedArmor )
    {
        // Assert
        Assert.Equal( expectedDamage, race.Damage );
        Assert.Equal( expectedHealth, race.Health );
        Assert.Equal( expectedArmor, race.Armor );
        Assert.Equal( expectedName, race.Name );
    }
}