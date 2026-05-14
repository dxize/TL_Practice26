namespace OrderManager
{
    internal class OrderApp
    {
        public void Run()
        {
            while ( true )
            {
                Order? order = ReadOrder();

                if ( order == null )
                {
                    continue;
                }

                if ( !IsOrderConfirmed( order ) )
                {
                    continue;
                }

                PrintSuccessMessage( order );

                if ( !ShouldContinueShopping() )
                {
                    Console.WriteLine( "До новых встреч!" );
                    break;
                }

                Console.WriteLine();
            }
        }

        private Order? ReadOrder()
        {
            string productName = ReadRequiredString( "Введите название товара: " );
            int amount = ReadPositiveInt( "Введите количество товара: " );
            string userName = ReadRequiredString( "Введите имя пользователя: " );
            string address = ReadRequiredString( "Введите адрес доставки: " );

            return new Order(
                productName,
                amount,
                userName,
                address,
                DateTime.Today.AddDays( 3 ) );
        }

        string ReadRequiredString( string message )
        {
            while ( true )
            {
                Console.Write( message );

                string? value = Console.ReadLine();

                if ( !string.IsNullOrWhiteSpace( value ) )
                {
                    return value.Trim();
                }

                Console.WriteLine( "Значение не должно быть пустым.\n" );
            }
        }

        int ReadPositiveInt( string message )
        {
            while ( true )
            {
                Console.Write( message );

                string? value = Console.ReadLine();

                if ( int.TryParse( value, out int number ) && number > 0 )
                {
                    return number;
                }

                Console.WriteLine( "Введите положительное целое число.\n" );
            }
        }

        private bool IsOrderConfirmed( Order order )
        {
            Console.WriteLine(
                $"Здравствуйте, {order.UserName}, вы заказали {order.Amount} {order.ProductName} " +
                $"на адрес {order.Address}, все верно? (Да, Нет)" );

            string? answer = Console.ReadLine();

            Console.WriteLine();

            return IsYesAnswer( answer );
        }

        private void PrintSuccessMessage( Order order )
        {
            Console.WriteLine(
                $"{order.UserName}! Ваш заказ {order.ProductName} в количестве {order.Amount} оформлен! " +
                $"Ожидайте доставку по адресу {order.Address} к {order.DeliveryDate:dd.MM.yyyy}\n" );
        }

        private bool ShouldContinueShopping()
        {
            Console.WriteLine( "Хотите ли вы продолжить покупки? (Да, Нет)" );

            string? answer = Console.ReadLine();

            return IsYesAnswer( answer );
        }

        private bool IsYesAnswer( string? answer )
        {
            return answer != null && answer.Trim().ToLower() == "да";
        }
    }
}