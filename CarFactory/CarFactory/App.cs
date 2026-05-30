using CarFactory.Model;

namespace CarFactory;

internal class App
{
    private readonly List<Car> _cars;
    private readonly CarFactory _factory;

    public App()
    {
        _cars = [];
        _factory = new CarFactory();
    }

    public void RunApp()
    {
        bool isRunning = true;

        while ( isRunning )
        {
            ShowStartMenu();
            isRunning = StartMenu();
        }
    }


    private void ShowStartMenu()
    {
        Console.WriteLine( """
            [1] - Выбрать конфигурацию для новой машины
            [2] - Посмотреть конфигурацию машин
            [3] - Выйти
            """ );
    }

    private bool StartMenu()
    {
        int choice = GetInputFromUser( 1, 3 );

        if ( choice == 1 )
        {
            Car newCar = _factory.CreateCar();
            _cars.Add( newCar );
        }
        else if ( choice == 2 )
        {
            ShowInfoCars();
        }
        else if ( choice == 3 )
        {
            return false;
        }

        return true;
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

            Console.WriteLine( $"\nОшибка! Введите цифру от {min} до {max}:" );
        }
    }

    private void ShowInfoCars()
    {
        if ( _cars.Count == 0 )
        {
            Console.WriteLine( "\nСписок машин пуст.\n" );
            return;
        }

        foreach ( Car car in _cars )
        {
            car.Info();
        }
    }
}