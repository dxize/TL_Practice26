using FighterGame.Model.Classes;

namespace Tests.Model.Classes;

public class ClassTests
{
    private const string Knight = "Knight";
    private const int KnightExpectedDamage = 8;
    private const int KnightExpectedHealth = 5;
    private const int KnightExpectedArmor = 5;

    private const string Peasant = "Peasant";
    private const int PeasantExpectedDamage = 6;
    private const int PeasantExpectedHealth = 2;
    private const int PeasantExpectedArmor = 2;

    private const string Wolf = "Wolf";
    private const int WolfExpectedDamage = 10;
    private const int WolfExpectedHealth = 6;
    private const int WolfExpectedArmor = 1;

    [Theory]
    [InlineData( Knight, KnightExpectedDamage, KnightExpectedHealth, KnightExpectedArmor )]
    [InlineData( Peasant, PeasantExpectedDamage, PeasantExpectedHealth, PeasantExpectedArmor )]
    [InlineData( Wolf, WolfExpectedDamage, WolfExpectedHealth, WolfExpectedArmor )]
    public void Constructor_WhenClassCreated_HasExpectedStats(
        string className,
        int expectedDamage,
        int expectedHealth,
        int expectedArmor )
    {
        // Arrange
        IClass classType;

        if ( className == Knight )
        {
            classType = new KnightClass();
        }
        else if ( className == Peasant )
        {
            classType = new PeasantClass();
        }
        else
        {
            classType = new WolfClass();
        }

        // Assert
        Assert.Equal( expectedDamage, classType.Damage );
        Assert.Equal( expectedHealth, classType.Health );
        Assert.Equal( expectedArmor, classType.Armor );
    }
}