using FighterGame;
using FighterGame.Model;
using FighterGame.Model.Armors;
using FighterGame.Model.Classes;
using FighterGame.Model.Races;
using FighterGame.Model.Weapons;
using Moq;

namespace Tests;

public class GameManagerTests
{
    [Fact]
    public void StartBattle_WhenFightersListIsEmpty_DoesNothing()
    {
        // Arrange
        List<IFighter> fighters = [];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Empty( fighters );
    }

    [Fact]
    public void StartBattle_WhenOnlyOneFighterExists_DoesNotAttack()
    {
        // Arrange
        Fighter fighter = CreateFighter( "Fighter 1", raceHealth: 50, classHealth: 50 );

        List<IFighter> fighters = [ fighter ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Equal( 100, fighter.Health );
    }

    [Fact]
    public void StartBattle_WhenTwoFightersExist_TargetTakesDamage()
    {
        // Arrange
        Fighter megaFighter = CreateFighter( "Mega", weaponDamage: 1000 );
        Fighter weakFighter = CreateFighter( "Weak", raceHealth: 1, classHealth: 0 );

        List<IFighter> fighters = [ megaFighter, weakFighter ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.False( weakFighter.IsAlive );
    }

    [Fact]
    public void StartBattle_WhenTwoFightersExist_RemovesDeadFighter()
    {
        // Arrange
        Fighter megaFighter = CreateFighter( "Mega", weaponDamage: 1000 );
        Fighter weakFighter = CreateFighter( "Weak", raceHealth: 1, classHealth: 0 );

        List<IFighter> fighters = [ megaFighter, weakFighter ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Single( fighters );
        Assert.Contains( megaFighter, fighters );
    }

    [Fact]
    public void StartBattle_WhenFighterHasHigherInitiative_AttacksFirst()
    {
        // Arrange
        Fighter fastFighter;
        Fighter slowFighter;

        do
        {
            fastFighter = CreateFighter( "Fast Mega", weaponDamage: 1000, raceHealth: 50, classHealth: 50 );
            slowFighter = CreateFighter( "Slow Mega", weaponDamage: 1000, raceHealth: 50, classHealth: 50 );
        }
        while ( fastFighter.Initiative <= slowFighter.Initiative );

        List<IFighter> fighters = [ slowFighter, fastFighter ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.False( slowFighter.IsAlive );
        Assert.Equal( 100, fastFighter.Health );
    }

    [Fact]
    public void StartBattle_WhenMoreThanTwoFightersExist_BattleResolvesToOneWinner()
    {
        // Arrange
        Fighter f1 = CreateFighter( "Fighter 1", weaponDamage: 1000, raceHealth: 10, classHealth: 0 );
        Fighter f2 = CreateFighter( "Fighter 2", weaponDamage: 1000, raceHealth: 10, classHealth: 0 );
        Fighter f3 = CreateFighter( "Fighter 3", weaponDamage: 1000, raceHealth: 10, classHealth: 0 );

        List<IFighter> fighters = [ f1, f2, f3 ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Single( fighters );
    }

    [Fact]
    public void StartBattle_WhenArmorExceedsDamage_BattleResolvesToOneWinner()
    {
        // Arrange
        Fighter f1 = CreateFighter( "Tank 1", weaponDamage: 0, raceHealth: 10, classHealth: 0, raceArmor: 100, classArmor: 100 );
        Fighter f2 = CreateFighter( "Tank 2", weaponDamage: 0, raceHealth: 10, classHealth: 0, raceArmor: 100, classArmor: 100 );

        List<IFighter> fighters = [ f1, f2 ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Single( fighters );
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
        Mock<IRace> raceMock = CreateRaceMock( raceHealth, raceDamage, raceArmor );
        Mock<IClass> classMock = CreateClassMock( classHealth, classDamage, classArmor );
        Mock<IWeapon> weaponMock = CreateWeaponMock( weaponDamage );
        Mock<IArmor> armorMock = CreateArmorMock( armorValue );

        return new Fighter(
            name,
            raceMock.Object,
            classMock.Object,
            weaponMock.Object,
            armorMock.Object
        );
    }

    private static Mock<IRace> CreateRaceMock( int health, int damage, int armor )
    {
        Mock<IRace> mock = new();
        mock.Setup( r => r.Health ).Returns( health );
        mock.Setup( r => r.Damage ).Returns( damage );
        mock.Setup( r => r.Armor ).Returns( armor );
        return mock;
    }

    private static Mock<IClass> CreateClassMock( int health, int damage, int armor )
    {
        Mock<IClass> mock = new();
        mock.Setup( c => c.Health ).Returns( health );
        mock.Setup( c => c.Damage ).Returns( damage );
        mock.Setup( c => c.Armor ).Returns( armor );
        return mock;
    }

    private static Mock<IWeapon> CreateWeaponMock( int damage )
    {
        Mock<IWeapon> mock = new();
        mock.Setup( w => w.Damage ).Returns( damage );
        return mock;
    }

    private static Mock<IArmor> CreateArmorMock( int armor )
    {
        Mock<IArmor> mock = new();
        mock.Setup( a => a.Armor ).Returns( armor );
        return mock;
    }
}