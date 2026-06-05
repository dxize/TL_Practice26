using FighterGame;
using FighterGame.Model;
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
        Mock<IFighter> fighterMock = CreateFighterMock(
            name: "Fighter 1",
            health: 100,
            fullDamage: 50,
            fullArmor: 0,
            initiative: 10,
            isAlive: true
        );

        List<IFighter> fighters = [ fighterMock.Object ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        fighterMock.Verify( f => f.TakeDamage( It.IsAny<int>() ), Times.Never );
    }

    [Fact]
    public void StartBattle_WhenTwoFightersExist_TargetTakesDamage()
    {
        // Arrange
        bool defenderAlive = true;

        Mock<IFighter> attackerMock = CreateFighterMock(
            name: "Attacker",
            health: 100,
            fullDamage: 100,
            fullArmor: 0,
            initiative: 20,
            isAlive: true
        );

        Mock<IFighter> defenderMock = CreateFighterMock(
            name: "Defender",
            health: 100,
            fullDamage: 10,
            fullArmor: 0,
            initiative: 10,
            isAliveGetter: () => defenderAlive
        );

        defenderMock
            .Setup( f => f.TakeDamage( It.IsAny<int>() ) )
            .Callback( () => defenderAlive = false );

        List<IFighter> fighters = [ attackerMock.Object, defenderMock.Object ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        defenderMock.Verify( f => f.TakeDamage( It.IsAny<int>() ), Times.Once );
    }

    [Fact]
    public void StartBattle_WhenTwoFightersExist_RemovesDeadFighter()
    {
        // Arrange
        bool defenderAlive = true;

        Mock<IFighter> attackerMock = CreateFighterMock(
            name: "Attacker",
            health: 100,
            fullDamage: 100,
            fullArmor: 0,
            initiative: 20,
            isAlive: true
        );

        Mock<IFighter> defenderMock = CreateFighterMock(
            name: "Defender",
            health: 100,
            fullDamage: 10,
            fullArmor: 0,
            initiative: 10,
            isAliveGetter: () => defenderAlive
        );

        defenderMock
            .Setup( f => f.TakeDamage( It.IsAny<int>() ) )
            .Callback( () => defenderAlive = false );

        List<IFighter> fighters = [ attackerMock.Object, defenderMock.Object ];
        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        Assert.Single( fighters );
        Assert.Contains( attackerMock.Object, fighters );
    }

    [Fact]
    public void StartBattle_WhenFighterHasHigherInitiative_AttacksFirst()
    {
        // Arrange
        bool lowInitiativeFighterAlive = true;

        Mock<IFighter> highInitiativeFighterMock = CreateFighterMock(
            name: "Fast Fighter",
            health: 100,
            fullDamage: 100,
            fullArmor: 0,
            initiative: 20,
            isAlive: true
        );

        Mock<IFighter> lowInitiativeFighterMock = CreateFighterMock(
            name: "Slow Fighter",
            health: 100,
            fullDamage: 100,
            fullArmor: 0,
            initiative: 5,
            isAliveGetter: () => lowInitiativeFighterAlive
        );

        lowInitiativeFighterMock
            .Setup( f => f.TakeDamage( It.IsAny<int>() ) )
            .Callback( () => lowInitiativeFighterAlive = false );

        List<IFighter> fighters =
        [
            lowInitiativeFighterMock.Object,
            highInitiativeFighterMock.Object
        ];

        GameManager gameManager = new( fighters );

        // Act
        gameManager.StartBattle();

        // Assert
        lowInitiativeFighterMock.Verify( f => f.TakeDamage( It.IsAny<int>() ), Times.Once );
        highInitiativeFighterMock.Verify( f => f.TakeDamage( It.IsAny<int>() ), Times.Never );
    }

    private static Mock<IFighter> CreateFighterMock(
        string name,
        int health,
        int fullDamage,
        int fullArmor,
        int initiative,
        bool isAlive )
    {
        Mock<IFighter> mock = new();

        mock.Setup( f => f.Name ).Returns( name );
        mock.Setup( f => f.Health ).Returns( health );
        mock.Setup( f => f.FullDamage ).Returns( fullDamage );
        mock.Setup( f => f.FullArmor ).Returns( fullArmor );
        mock.Setup( f => f.Initiative ).Returns( initiative );
        mock.Setup( f => f.IsAlive ).Returns( isAlive );

        return mock;
    }

    private static Mock<IFighter> CreateFighterMock(
        string name,
        int health,
        int fullDamage,
        int fullArmor,
        int initiative,
        Func<bool> isAliveGetter )
    {
        Mock<IFighter> mock = new();

        mock.Setup( f => f.Name ).Returns( name );
        mock.Setup( f => f.Health ).Returns( health );
        mock.Setup( f => f.FullDamage ).Returns( fullDamage );
        mock.Setup( f => f.FullArmor ).Returns( fullArmor );
        mock.Setup( f => f.Initiative ).Returns( initiative );
        mock.Setup( f => f.IsAlive ).Returns( isAliveGetter );

        return mock;
    }
}