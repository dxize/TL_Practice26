using FighterGame.Model;
using FighterGame.Model.Armors;
using FighterGame.Model.Classes;
using FighterGame.Model.Races;
using FighterGame.Model.Weapons;
using Moq;

namespace Tests.Model;

public class FighterTests
{
    [Fact]
    public void Constructor_WhenArgumentsAreValid_SetsName()
    {
        // Arrange
        string expectedName = "Test Fighter";

        // Act
        Fighter fighter = CreateFighter( name: expectedName );

        // Assert
        Assert.Equal( expectedName, fighter.Name );
    }

    [Fact]
    public void Constructor_WhenArgumentsAreValid_CalculatesHealthFromRaceAndClass()
    {
        // Arrange
        int raceHealth = 100;
        int classHealth = 50;
        int expectedHealth = 150;

        // Act
        Fighter fighter = CreateFighter(
            raceHealth: raceHealth,
            classHealth: classHealth
        );

        // Assert
        Assert.Equal( expectedHealth, fighter.Health );
    }

    [Fact]
    public void Constructor_WhenArgumentsAreValid_SetsInitiativeInRange()
    {
        // Arrange and Act
        Fighter fighter = CreateFighter();

        // Assert
        Assert.InRange( fighter.Initiative, 1, 20 );
    }

    [Fact]
    public void Constructor_WhenArgumentsAreValid_Alive()
    {
        // Arrange and Act
        Fighter fighter = CreateFighter();

        // Assert
        Assert.True( fighter.IsAlive );
    }

    [Fact]
    public void FullDamage_WhenCalled_ReturnsRaceClassAndWeaponDamageSum()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceDamage: 10,
            classDamage: 15,
            weaponDamage: 20
        );

        // Act
        int fullDamage = fighter.FullDamage;

        // Assert
        Assert.Equal( 45, fullDamage );
    }

    [Fact]
    public void FullArmor_WhenCalled_ReturnsRaceClassAndArmorSum()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceArmor: 5,
            classArmor: 3,
            armorValue: 10
        );

        // Act
        int fullArmor = fighter.FullArmor;

        // Assert
        Assert.Equal( 18, fullArmor );
    }

    public static readonly TheoryData<int, int> TakeDamageTestData = new()
    {
        { 10, 140 },
        { 50, 100 },
        { 149, 1 }
    };

    [Theory]
    [MemberData( nameof( TakeDamageTestData ) )]
    public void TakeDamage_WhenDamageIsPositive_DecreasesHealth(
        int damage,
        int expectedHealth )
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( damage );

        // Assert
        Assert.Equal( expectedHealth, fighter.Health );
    }

    [Fact]
    public void TakeDamage_WhenDamageIsZero_DoesNotChangeHealth()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( 0 );

        // Assert
        Assert.Equal( 150, fighter.Health );
    }

    [Fact]
    public void TakeDamage_WhenDamageIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => fighter.TakeDamage( -10 ) );
    }

    [Fact]
    public void TakeDamage_WhenDamageEqualsHealth_SetsHealthToZero()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( 150 );

        // Assert
        Assert.Equal( 0, fighter.Health );
    }

    [Fact]
    public void TakeDamage_WhenDamageGreaterThanHealth_SetsHealthToZero()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( 200 );

        // Assert
        Assert.Equal( 0, fighter.Health );
    }

    [Fact]
    public void TakeDamage_WhenHealthBecomesZero_BecomesDead()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( 150 );

        // Assert
        Assert.False( fighter.IsAlive );
    }

    [Fact]
    public void TakeDamage_WhenHealthRemainsPositive_RemainsAlive()
    {
        // Arrange
        Fighter fighter = CreateFighter(
            raceHealth: 100,
            classHealth: 50
        );

        // Act
        fighter.TakeDamage( 50 );

        // Assert
        Assert.True( fighter.IsAlive );
    }

    private static Fighter CreateFighter(
        string name = "Test Fighter",
        int raceHealth = 100,
        int classHealth = 50,
        int raceDamage = 10,
        int classDamage = 15,
        int weaponDamage = 20,
        int raceArmor = 5,
        int classArmor = 3,
        int armorValue = 10 )
    {
        Mock<IRace> raceMock = CreateRaceMock(
            health: raceHealth,
            damage: raceDamage,
            armor: raceArmor
        );

        Mock<IClass> classMock = CreateClassMock(
            health: classHealth,
            damage: classDamage,
            armor: classArmor
        );

        Mock<IWeapon> weaponMock = CreateWeaponMock(
            damage: weaponDamage
        );

        Mock<IArmor> armorMock = CreateArmorMock(
            armor: armorValue
        );

        return new(
            name,
            raceMock.Object,
            classMock.Object,
            weaponMock.Object,
            armorMock.Object
        );
    }

    private static Mock<IRace> CreateRaceMock(
        int health = 100,
        int damage = 10,
        int armor = 5 )
    {
        Mock<IRace> mock = new();

        mock.Setup( r => r.Health ).Returns( health );
        mock.Setup( r => r.Damage ).Returns( damage );
        mock.Setup( r => r.Armor ).Returns( armor );

        return mock;
    }

    private static Mock<IClass> CreateClassMock(
        int health = 50,
        int damage = 15,
        int armor = 3 )
    {
        Mock<IClass> mock = new();

        mock.Setup( c => c.Health ).Returns( health );
        mock.Setup( c => c.Damage ).Returns( damage );
        mock.Setup( c => c.Armor ).Returns( armor );

        return mock;
    }

    private static Mock<IWeapon> CreateWeaponMock( int damage = 20 )
    {
        Mock<IWeapon> mock = new();

        mock.Setup( w => w.Damage ).Returns( damage );

        return mock;
    }

    private static Mock<IArmor> CreateArmorMock( int armor = 10 )
    {
        Mock<IArmor> mock = new();

        mock.Setup( a => a.Armor ).Returns( armor );

        return mock;
    }
}