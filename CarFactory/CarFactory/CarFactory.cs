using CarFactory.Model;
using CarFactory.Model.CarBodyTypes;
using CarFactory.Model.CarEngines;
using CarFactory.Model.Transmissions;

namespace CarFactory
{
    internal class CarFactory
    {
        private List<ITransmission> availableTransmission = [
            new AutomaticTransmission(),
            new ManualTransmission()
        ];

        private List<ICarEngine> availableEngine = [
            new DieselEngine(),
            new PetrolEngine()
        ];

        private List<ICarBodyType> availableCarBodyType = [
            new CoupeCarBodyType(),
            new SedanCarBodyType(),
            new TruckCarBodyType()
        ];

        private List<string> availableColor = [
            "Синий",
            "Белый",
            "Черный",
            "Серый",
            "Желтый"
        ];

        public Car CreateCar()
        {
            Console.WriteLine( "\n=== Создание нового автомобиля ===" );

            string name = AskName();

            Console.WriteLine( "Выберите кузов из списка ниже:" );
            ICarBodyType selectedCarBodyType = SelectOption( availableCarBodyType, x => x.Name );

            Console.WriteLine( "\nВыберите цвет списка ниже:" );
            string selectedColor = SelectOption( availableColor, x => x );

            Console.WriteLine( "\nВыберите тип коробки передач из списка ниже:" );
            ITransmission selectedTransmission = SelectOption( availableTransmission, x => x.Name );

            Console.WriteLine( "\nВыберите тип двигателя из списка ниже:" );
            ICarEngine selectedEngine = SelectOption( availableEngine, x => x.Name );

            Console.WriteLine( $"\nАвтомобиль {name} успешно добавлен!\n" );

            return new Car( name, selectedCarBodyType, selectedColor, selectedTransmission, selectedEngine );

        }

        private string AskName()
        {
            string name = string.Empty;
            bool isCorrectName = false;
            while ( !isCorrectName )
            {
                Console.Write( "Введите марку авто: " );
                name = Console.ReadLine();
                Console.WriteLine();
                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    Console.WriteLine( "Название не может быть пустым\n" );
                    continue;
                }

                isCorrectName = true;
            }

            return name;
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

                Console.WriteLine( "\nОшибка! Введите корректную цифру из списка." );
            }
        }
    }
}