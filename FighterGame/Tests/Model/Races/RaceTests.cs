using FighterGame.Model.Races;

namespace Tests.Model.Races;

public class RaceTests
{
    private const string Asian = "Asian";
    private const int AsianExpectedDamage = 8;
    private const int AsianExpectedHealth = 70;
    private const int AsianExpectedArmor = 10;

    private const string European = "European";
    private const int EuropeanExpectedDamage = 10;
    private const int EuropeanExpectedHealth = 90;
    private const int EuropeanExpectedArmor = 4;

    private const string Negroid = "Negroid";
    private const int NegroidExpectedDamage = 12;
    private const int NegroidExpectedHealth = 110;
    private const int NegroidExpectedArmor = 1;

    [Theory]
    [InlineData( Asian, AsianExpectedDamage, AsianExpectedHealth, AsianExpectedArmor )]
    [InlineData( European, EuropeanExpectedDamage, EuropeanExpectedHealth, EuropeanExpectedArmor )]
    [InlineData( Negroid, NegroidExpectedDamage, NegroidExpectedHealth, NegroidExpectedArmor )]
    public void Constructor_WhenRaceCreated_HasExpectedStats(
        string raceName,
        int expectedDamage,
        int expectedHealth,
        int expectedArmor )
    {
        // Arrange
        IRace race;

        if ( raceName == Asian )
        {
            race = new AsianRace();
        }
        else if ( raceName == European )
        {
            race = new EuropeanRace();
        }
        else
        {
            race = new NegroidRace();
        }

        // Assert
        Assert.Equal( expectedDamage, race.Damage );
        Assert.Equal( expectedHealth, race.Health );
        Assert.Equal( expectedArmor, race.Armor );
    }
}