using FighterGame.Model;

namespace FighterGame;

internal class App
{
    private List<IFighter> _fighters = [];
    private FighterFactory _creator = new();

    public void RunApp()
    {
        while ( true )
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
    }

    private void ShowWelcomeText()
    {
        Console.WriteLine(
            """
            ~~ ~~ Игра Файт Гейм ~~ ~~

            [1] - Арена (Добавление бойцов и бои)
            [2] - Правила игры
            [3] - Выход

            """ );
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
        Console.WriteLine(
            """
            ~~ ~~ Правила игры ~~ ~~

            Игра представляет собой боевую систему между персонажами.

            Правила боя:
            - Бой проходит по раундам.
            - Урон = max(Сила атакующего - Броня защищающегося, 0).
            - Урон может случайно изменяться (-20% до +10%).
            - Возможны критические удары (x2 урон).
            - Побеждает боец, у которого осталось здоровье.

            Нажмите Enter для продолжения...

            """ );

        Console.ReadLine();
    }


    private void ShowArenaMenu()
    {
        Console.WriteLine(
            $"""
            --- АРЕНА ---
            Текущее количество бойцов на арене: {_fighters.Count}

            [1] - Добавить нового бойца
            [2] - Начать битву
            [3] - Вернуться в главное меню

            """ );
    }

    private void ArenaMenu()
    {
        while ( true )
        {
            ShowArenaMenu();

            int choice = GetInputFromUser( 1, 3 );

            if ( choice == 1 )
            {
                IFighter newFighter = _creator.Create();
                _fighters.Add( newFighter );
            }
            else if ( choice == 2 )
            {
                if ( _fighters.Count < 2 )
                {
                    Console.WriteLine( "\nДля начала битвы нужно как минимум 2 бойца!" );
                    continue;
                }

                GameManager manager = new GameManager( _fighters );
                manager.StartBattle();

                _fighters.Clear();
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