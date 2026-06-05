using FighterGame.Model.Armors;

namespace Tests.Model.Armors;

public class ArmorTests
{
    private const string NoArmorName = "NoArmor";
    private const int NoArmorExpectedArmor = 0;

    private const string FirstArmorName = "FirstArmor";
    private const int FirstArmorExpectedArmor = 2;

    private const string SecondArmorName = "SecondArmor";
    private const int SecondArmorExpectedArmor = 4;

    private const string ThirdArmorName = "ThirdArmor";
    private const int ThirdArmorExpectedArmor = 6;

    [Theory]
    [InlineData( NoArmorName, NoArmorExpectedArmor )]
    [InlineData( FirstArmorName, FirstArmorExpectedArmor )]
    [InlineData( SecondArmorName, SecondArmorExpectedArmor )]
    [InlineData( ThirdArmorName, ThirdArmorExpectedArmor )]
    public void Constructor_WhenArmorCreated_HasExpectedArmor(
        string armorName,
        int expectedArmor )
    {
        // Arrange
        IArmor armor;

        if ( armorName == NoArmorName )
        {
            armor = new NoArmor();
        }
        else if ( armorName == FirstArmorName )
        {
            armor = new FirstArmor();
        }
        else if ( armorName == SecondArmorName )
        {
            armor = new SecondArmor();
        }
        else
        {
            armor = new ThirdArmor();
        }

        // Assert
        Assert.Equal( expectedArmor, armor.Armor );
    }
}