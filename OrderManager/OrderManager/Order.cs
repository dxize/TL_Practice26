namespace OrderManager;

internal class Order
{
    public string ProductName { get; }
    public int Amount { get; }
    public string UserName { get; }
    public string Address { get; }
    public DateTime DeliveryDate { get; }

    public Order( string productName, int amount, string userName, string address, DateTime deliveryDate )
    {
        ProductName = productName;
        Amount = amount;
        UserName = userName;
        Address = address;
        DeliveryDate = deliveryDate;
    }
}