namespace Domain.Entities;

public class Reservation
{
    public int Id { get; private init; }

    public int PropertyId { get; private set; }
    public Property Property { get; private set; }

    public int RoomTypeId { get; private set; }
    public RoomType RoomType { get; private set; }

    public DateTime ArrivalDate { get; private set; }
    public DateTime DepartureDate { get; private set; }

    public TimeSpan ArrivalTime { get; private set; }
    public TimeSpan DepartureTime { get; private set; }

    public string GuestName { get; private set; }
    public string GuestPhoneNumber { get; private set; }

    public int Guests { get; private set; }

    public decimal Total { get; private set; }
    public string Currency { get; private set; }

    public bool IsCanceled { get; private set; }

    public Reservation(
        int propertyId,
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate,
        TimeSpan arrivalTime,
        TimeSpan departureTime,
        string guestName,
        string guestPhoneNumber,
        int guests,
        decimal total,
        string currency )
    {
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        ArrivalDate = arrivalDate.Date;
        DepartureDate = departureDate.Date;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
        Guests = guests;
        Total = total;
        Currency = currency;
        IsCanceled = false;
    }

    private Reservation()
    {
    }

    public void Cancel()
    {
        IsCanceled = true;
    }
}