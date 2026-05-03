internal class App
{
    private List<IFighter> m_fighters = new List<IFighter>();
    private CreateFighter m_creator = new CreateFighter();

    public void RunApp()
    {
        ShowWelcomeText();
        int input = GetInputFromUser( 1, 3 );

        if ( input == 1 )
        {
            ArenaMenu();
        }
        else if ( input == 2 )
        {
            GetInfoGame();
        }
        else if ( input == 3 )
        {
            Environment.Exit( 0 );
        }
    }

    private void ShowWelcomeText()
    {
        Console.WriteLine( "~~ ~~ Игра Файт Гейм ~~ ~~\n" );
        Console.WriteLine( "[1] - Арена (Добавление бойцов и бои)" );
        Console.WriteLine( "[2] - Правила игры" );
        Console.WriteLine( "[3] - Выход\n" );
    }

    private int GetInputFromUser( int min, int max )
    {
        while ( true )
        {
            string input = Console.ReadLine();

            if ( int.TryParse( input, out int result ) && result >= min && result <= max )
            {
                return result;
            }

            Console.WriteLine( $"Ошибка! Введите цифру от {min} до {max}:" );
        }
    }

    private void GetInfoGame()
    {
        Console.WriteLine( "\n~~ ~~ Правила игры ~~ ~~\n" );
        Console.WriteLine( "Игра представляет собой боевую систему между персонажами.\n" );
        Console.WriteLine( "Правила боя:" );
        Console.WriteLine( "- Бой проходит по раундам." );
        Console.WriteLine( "- Урон = max(Сила атакующего - Броня защищающегося, 0)." );
        Console.WriteLine( "- Урон может случайно изменяться (-20% до +10%)." );
        Console.WriteLine( "- Возможны критические удары (x2 урон)." );
        Console.WriteLine( "- Побеждает боец, у которого осталось здоровье.\n" );
        Console.WriteLine( "Нажмите Enter для продолжения...\n" );
        Console.ReadLine();
    }


    private void ShowArenaMenu()
    {
        Console.WriteLine( "\n--- АРЕНА ---" );
        Console.WriteLine( $"Текущее количество бойцов на арене: {m_fighters.Count}" );
        Console.WriteLine( "[1] - Добавить нового бойца" );
        Console.WriteLine( "[2] - Начать битву" );
        Console.WriteLine( "[3] - Вернуться в главное меню" );
    }

    private void ArenaMenu()
    {
        while ( true )
        {
            ShowArenaMenu();

            int choice = GetInputFromUser( 1, 3 );

            if ( choice == 1 )
            {
                IFighter newFighter = m_creator.Create();
                m_fighters.Add( newFighter );
            }
            else if ( choice == 2 )
            {
                if ( m_fighters.Count < 2 )
                {
                    Console.WriteLine( "\nДля начала битвы нужно как минимум 2 бойца!" );
                    continue;
                }

                GameManager manager = new GameManager( m_fighters );
                manager.StartBattle();

                m_fighters.Clear();
                Console.WriteLine( "\nАрена очищена. Нажмите Enter для продолжения..." );
                Console.ReadLine();
            }
            else if ( choice == 3 )
            {
                return;
            }
        }
    }
}