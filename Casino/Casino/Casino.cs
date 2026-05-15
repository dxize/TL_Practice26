namespace CasinoGame;

internal class Casino
{
    private const uint MIN_WIN_SEED = 18;
    private const uint MAX_WIN_SEED = 20;
    private const uint WIN_MULTIPLICATOR = 25;

    private decimal _balance;
    private bool _isGameFinished;

    public void Run()
    {
        PrintHeader();

        while ( !_isGameFinished )
        {
            PrintMenu();

            Console.WriteLine();
            Console.Write( "Выберите пункт меню: " );

            string option = Console.ReadLine();

            HandleOption( option );

            Console.WriteLine();
        }
    }

    private void PrintHeader()
    {
        Console.WriteLine( "Casino Game" );
    }

    private void PrintMenu()
    {
        Console.WriteLine(
            """
            1. пополнить баланс
            2. показать баланс
            3. играть
            4. выйти
            """ );
    }

    private void HandleOption( string option )
    {
        switch ( option )
        {
            case "1":
                MakeDeposit();
                break;

            case "2":
                ShowBalance();
                break;

            case "3":
                Play();
                break;

            case "4":
                Exit();
                break;

            default:
                Console.WriteLine( "Ошибка: неизвестный пункт меню." );
                break;
        }
    }

    private void MakeDeposit()
    {
        Console.Write( "Введите сумму пополнения баланса: " );

        string depositString = Console.ReadLine();

        if ( !decimal.TryParse( depositString, out decimal deposit ) || deposit <= 0 )
        {
            Console.WriteLine( "Ошибка: сумма пополнения должна быть положительным числом." );
            return;
        }

        if ( decimal.MaxValue - deposit < _balance )
        {
            Console.WriteLine( "Ошибка: слишком большая сумма пополнения." );
            return;
        }

        _balance += deposit;

        Console.WriteLine( $"Баланс пополнен на {deposit}." );
        ShowBalance();
    }

    private void ShowBalance()
    {
        Console.WriteLine( $"Текущий баланс: {_balance}" );
    }

    private void Play()
    {
        if ( _balance <= 0 )
        {
            Console.WriteLine( "Ошибка: сначала пополните баланс." );
            return;
        }

        Console.Write( "Введите ставку: " );

        string betString = Console.ReadLine();

        if ( !decimal.TryParse( betString, out decimal bet ) || bet <= 0 )
        {
            Console.WriteLine( "Ошибка: ставка должна быть положительным числом." );
            return;
        }

        if ( bet > _balance )
        {
            Console.WriteLine( "Ошибка: ставка не может быть больше баланса." );
            return;
        }

        uint seed = ( uint )Random.Shared.Next( 1, 21 );

        if ( IsWinningSeed( seed ) )
        {
            decimal winAmount = CalculateWinAmount( bet, seed );

            _balance += winAmount;

            Console.WriteLine( $"Вы выиграли! Выпало число {seed}." );
            Console.WriteLine( $"Сумма выигрыша: {winAmount}" );
            ShowBalance();

            return;
        }

        _balance -= bet;

        Console.WriteLine( $"Вы проиграли. Выпало число {seed}." );
        Console.WriteLine( $"Списана ставка: {bet}" );
        ShowBalance();
    }

    private bool IsWinningSeed( uint seed )
    {
        return seed >= MIN_WIN_SEED && seed <= MAX_WIN_SEED;
    }

    private decimal CalculateWinAmount( decimal bet, uint seed )
    {
        uint winPercent = WIN_MULTIPLICATOR * ( seed % 17u );

        return bet * winPercent / 100m;
    }

    private void Exit()
    {
        _isGameFinished = true;

        Console.WriteLine( "Игра завершена" );
    }
}