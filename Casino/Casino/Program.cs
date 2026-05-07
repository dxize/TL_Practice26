double balance = 0;
bool isGameFinished = false;
PrintHeader();


while ( !isGameFinished )
{
    PrintMenu();
    Console.WriteLine();
    string option = Console.ReadLine();
    OptionHandleResult result = HandleOptions( option );
    Console.WriteLine( result );
    Console.WriteLine();
}


void PrintHeader()
{
    const string header = "Casino Game";

    Console.WriteLine( header );
}

void PrintMenu()
{
    List<string> menuOptions = [
        "1. пополнить баланс",
        "2. показать баланс",
        "3. играть",
        "4. выйти"];

    foreach ( string option in menuOptions )
    {
        Console.WriteLine( option );
    }
}

OptionHandleResult HandleOptions( string option )
{
    switch ( option )
    {
        case "1":
            return MakeDeposit();

        case "2":
            return ShowBalance();

        case "3":
            return Play();

        case "4":
            return Exit();

        default:
            return OptionHandleResult.InvalidOption;

    }

    return OptionHandleResult.Success;
}

OptionHandleResult MakeDeposit()
{
    Console.WriteLine( "Введите объём средств для пополнения баланса: " );
    string depositString = Console.ReadLine();
    if ( !int.TryParse( depositString, out int deposit ) || deposit <= 0 )
    {
        return OptionHandleResult.InvalidDepositValue;
    }


    if ( int.MaxValue - deposit < balance )
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    balance += deposit;
    return OptionHandleResult.Success;
}

OptionHandleResult ShowBalance()
{
    Console.WriteLine( $"Текущий баланс {balance}" );
    return OptionHandleResult.Success;
}

OptionHandleResult Play()
{
    Console.Write( "Введи ставку: " );

    string betStr = Console.ReadLine();

    if ( !int.TryParse( betStr, out int bet ) || bet <= 0 )
    {
        return OptionHandleResult.InvalidBet;
    }

    if ( bet >= balance )
    {
        return OptionHandleResult.InvalidBet;
    }

    int seed = Random.Shared.Next( 1, 21 );
    if ( seed >= 18 && seed <= 20 )
    {
        double winAmount = CalculateWinAmount( bet, seed );
        balance += winAmount;
    }
    else
    {
        balance -= bet;
    }

    return OptionHandleResult.Success;
}

double CalculateWinAmount( int bet, int seed )
{
    const int multiplicator = 25;

    int winPrecent = multiplicator * ( seed % 17 );
    if ( winPrecent <= 0 )
    {
        return 0;
    }

    return bet * ( winPrecent / 100 );
}

OptionHandleResult Exit()
{
    isGameFinished = true;
    return OptionHandleResult.Success;
}


enum OptionHandleResult
{
    Success = 0,
    InvalidOption = 1,
    InvalidDepositValue = 2,
    InvalidBet = 3
}

