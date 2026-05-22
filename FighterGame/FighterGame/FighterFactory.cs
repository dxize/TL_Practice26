using FighterGame.Model;
using FighterGame.Model.Armors;
using FighterGame.Model.Classes;
using FighterGame.Model.Races;
using FighterGame.Model.Weapons;

namespace FighterGame;

internal class FighterFactory
{
    private readonly List<IRace> _availableRaces =
    [
        new AsianRace(),
        new EuropeanRace(),
        new NegroidRace()
    ];

    private readonly List<IClass> _availableClasses =
    [
        new KnightClass(),
        new PeasantClass(),
        new WolfClass()
    ];

    private readonly List<IWeapon> _availableWeapons =
    [
        new HandGunWeapon(),
        new SpearWeapon(),
        new SwordWeapon()
    ];

    private readonly List<IArmor> _availableArmors =
    [
        new FirstArmor(),
        new NoArmor(),
        new SecondArmor(),
        new ThirdArmor()
    ];

    public IFighter Create()
    {
        Console.WriteLine( "\n=== Создание нового бойца ===" );

        Console.Write( "Введите имя персонажа: " );
        string name = Console.ReadLine();
        Console.WriteLine();
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            name = "Безымянный";
        }

        Console.WriteLine( "\nВыберите расу из списка ниже:" );
        IRace selectedRace = SelectOption( _availableRaces, r => r.Name );

        Console.WriteLine( "\nВыберите класс из списка ниже:" );
        IClass selectedClass = SelectOption( _availableClasses, c => c.Name );

        Console.WriteLine( "\nВыберите оружие из списка ниже:" );
        IWeapon selectedWeapon = SelectOption( _availableWeapons, w => w.Name );

        Console.WriteLine( "\nВыберите броню из списка ниже:" );
        IArmor selectedArmor = SelectOption( _availableArmors, a => a.Name );

        Console.WriteLine( $"Боец {name} успешно добавлен!\n" );

        return new Fighter( name, selectedRace, selectedClass, selectedWeapon, selectedArmor );
    }

    private T SelectOption<T>( List<T> options, Func<T, string> getName )
    {
        for ( int i = 0; i < options.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {getName( options[ i ] )}" );
        }

        while ( true )
        {
            string input = Console.ReadLine();

            if ( int.TryParse( input, out int choice ) && choice >= 1 && choice <= options.Count )
            {
                return options[ choice - 1 ];
            }

            Console.WriteLine( "Ошибка! Введите корректную цифру из списка." );
        }
    }
}