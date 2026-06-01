using FighterGame;
using FighterGame.Model;

namespace Tests;

public class FighterFactoryTests
{
    [Fact]
    public void Create_ValidInput_ReturnsFighterWithExpectedStats()
    {
        // Arrange
        string input = """
                       Test Fighter
                       1
                       1
                       1
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( "Test Fighter", fighter.Name );
        Assert.Equal( 75, fighter.Health );
        Assert.Equal( 46, fighter.FullDamage );
        Assert.Equal( 17, fighter.FullArmor );
    }

    [Fact]
    public void Create_EmptyName_UsesDefaultName()
    {
        // Arrange
        string input = """
                       
                       1
                       1
                       1
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( "Безымянный", fighter.Name );
    }

    [Fact]
    public void Create_WhitespaceName_UsesDefaultName()
    {
        // Arrange
        string input = """
                          
                       1
                       1
                       1
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( "Безымянный", fighter.Name );
    }

    [Fact]
    public void Create_InvalidRaceInputThenValidInput_ReturnsFighter()
    {
        // Arrange
        string input = """
                       Test Fighter
                       0
                       abc
                       2
                       1
                       1
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( 95, fighter.Health );
        Assert.Equal( 48, fighter.FullDamage );
        Assert.Equal( 11, fighter.FullArmor );
    }

    [Fact]
    public void Create_InvalidClassInputThenValidInput_ReturnsFighter()
    {
        // Arrange
        string input = """
                       Test Fighter
                       1
                       0
                       abc
                       2
                       1
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( 72, fighter.Health );
        Assert.Equal( 44, fighter.FullDamage );
        Assert.Equal( 14, fighter.FullArmor );
    }

    [Fact]
    public void Create_InvalidWeaponInputThenValidInput_ReturnsFighter()
    {
        // Arrange
        string input = """
                       Test Fighter
                       1
                       1
                       0
                       abc
                       2
                       1

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( 75, fighter.Health );
        Assert.Equal( 31, fighter.FullDamage );
        Assert.Equal( 17, fighter.FullArmor );
    }

    [Fact]
    public void Create_InvalidArmorInputThenValidInput_ReturnsFighter()
    {
        // Arrange
        string input = """
                       Test Fighter
                       1
                       1
                       1
                       0
                       abc
                       4

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( 75, fighter.Health );
        Assert.Equal( 46, fighter.FullDamage );
        Assert.Equal( 21, fighter.FullArmor );
    }

    [Fact]
    public void Create_LastOptionsSelected_ReturnsFighterWithExpectedStats()
    {
        // Arrange
        string input = """
                       Test Fighter
                       3
                       3
                       3
                       4

                       """;

        // Act
        IFighter fighter = CreateFighterFromConsoleInput( input );

        // Assert
        Assert.Equal( 116, fighter.Health );
        Assert.Equal( 32, fighter.FullDamage );
        Assert.Equal( 8, fighter.FullArmor );
    }

    private static IFighter CreateFighterFromConsoleInput( string input )
    {
        TextReader originalInput = Console.In;
        TextWriter originalOutput = Console.Out;

        try
        {
            Console.SetIn( new StringReader( input ) );
            Console.SetOut( new StringWriter() );

            FighterFactory factory = new();

            return factory.Create();
        }
        finally
        {
            Console.SetIn( originalInput );
            Console.SetOut( originalOutput );
        }
    }
}