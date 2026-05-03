internal class CreateFighter
{
    private List<IRace> availableRaces = new List<IRace>
    {
        new AzaiteRace(),
        new EuropeanRace(),
        new NegroidRace()
    };

    private List<IClass> availableClasses = new List<IClass>
    {
        new KnightClass(),
        new PeasantClass(),
        new WolfClass()
    };

    private List<IWeapon> availableWeapons = new List<IWeapon>
    {
        new HandGunWeapon(),
        new SpearWeapon(),
        new SwordWeapon()
    };

    private List<IArmor> availableArmors = new List<IArmor>
    {
        new FirstArmor(),
        new NoArmor(),
        new SecondArmor(),
        new ThirdArmor()
    };

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
        IRace selectedRace = SelectOption( availableRaces, r => r.GetType().Name );

        Console.WriteLine( "\nВыберите класс из списка ниже:" );
        IClass selectedClass = SelectOption( availableClasses, c => c.GetType().Name );

        Console.WriteLine( "\nВыберите оружие из списка ниже:" );
        IWeapon selectedWeapon = SelectOption( availableWeapons, w => w.GetType().Name );

        Console.WriteLine( "\nВыберите броню из списка ниже:" );
        IArmor selectedArmor = SelectOption( availableArmors, a => a.GetType().Name );

        Console.WriteLine( $"Боец {name} успешно добавлен!\n" );

        return new Fighter( name, selectedRace, selectedClass, selectedWeapon, selectedArmor );
    }

    private T SelectOption<T>( List<T> options, Func<T, string> getName )
    {
        for ( int i = 0; i < options.Count; i++ )
        {
            string displayName = getName( options[ i ] )
                .Replace( "Race", "" )
                .Replace( "Class", "" )
                .Replace( "Weapon", "" )
                .Replace( "Armor", "" );

            Console.WriteLine( $"{i + 1} - {displayName}" );
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