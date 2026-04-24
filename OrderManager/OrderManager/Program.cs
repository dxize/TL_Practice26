using System;

bool isStopApp = false;

while ( !isStopApp )
{
    Order? order = ReadOrder();

    if ( order == null )
    {
        continue;
    }

    if ( !ConfirmOrder( order ) )
    {
        continue;
    }

    PrintSuccessMessage( order );

    if ( !AskContinueShopping() )
    {
        Console.WriteLine( "До новых встреч!" );
        isStopApp = true;
    }

    Console.WriteLine();
}

Order? ReadOrder()
{
    string? productName = ReadRequiredString( "Введите название товара: " );

    if ( productName == null )
    {
        return null;
    }

    int? amount = ReadPositiveInt( "Введите количество товара: " );

    if ( amount == null )
    {
        return null;
    }

    string? userName = ReadRequiredString( "Введите имя пользователя: " );

    if ( userName == null )
    {
        return null;
    }

    string? address = ReadRequiredString( "Введите адрес доставки: " );

    if ( address == null )
    {
        return null;
    }

    return new Order
    {
        ProductName = productName,
        Amount = amount.Value,
        UserName = userName,
        Address = address,
        DeliveryDate = DateTime.Today.AddDays( 3 )
    };
}

string? ReadRequiredString( string message )
{
    Console.Write( message );

    string? value = Console.ReadLine();

    if ( string.IsNullOrWhiteSpace( value ) )
    {
        Console.WriteLine( "Значение не должно быть пустым.\n" );
        return null;
    }

    return value.Trim();
}

int? ReadPositiveInt( string message )
{
    Console.Write( message );

    string? value = Console.ReadLine();

    if ( !int.TryParse( value, out int number ) || number <= 0 )
    {
        Console.WriteLine( "Введите положительное целое число.\n" );
        return null;
    }

    return number;
}

bool ConfirmOrder( Order order )
{
    Console.WriteLine(
        $"Здравствуйте, {order.UserName}, вы заказали {order.Amount} {order.ProductName} " +
        $"на адрес {order.Address}, все верно? (Да, Нет)" );

    string? answer = Console.ReadLine();

    Console.WriteLine();

    return IsYesAnswer( answer );
}

void PrintSuccessMessage( Order order )
{
    Console.WriteLine(
        $"{order.UserName}! Ваш заказ {order.ProductName} в количестве {order.Amount} оформлен! " +
        $"Ожидайте доставку по адресу {order.Address} к {order.DeliveryDate:dd.MM.yyyy}\n" );
}

bool AskContinueShopping()
{
    Console.WriteLine( "Хотите ли вы продолжить покупки? (Да, Нет)" );

    string? answer = Console.ReadLine();

    return IsYesAnswer( answer );
}

bool IsYesAnswer( string? answer )
{
    return answer != null && answer.Trim().ToLower() == "да";
}

class Order
{
    public string ProductName { get; set; } = "";
    public int Amount { get; set; }
    public string UserName { get; set; } = "";
    public string Address { get; set; } = "";
    public DateTime DeliveryDate { get; set; }
}