namespace Domain.Entities;

public class RoomType
{
    public int Id { get; private init; }

    public int PropertyId { get; private set; }
    public Property Property { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public decimal DailyPrice { get; private set; }
    public string Currency { get; private set; } = string.Empty;

    public int MinPersonCount { get; private set; }
    public int MaxPersonCount { get; private set; }

    public int TotalRooms { get; private set; }

    public string Services { get; private set; } = string.Empty;
    public string Amenities { get; private set; } = string.Empty;

    public List<Reservation> Reservations { get; private init; } = new();

    public RoomType(
        int propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int totalRooms,
        string services,
        string amenities )
    {
        PropertyId = propertyId;

        SetName( name );
        SetDailyPrice( dailyPrice );
        SetCurrency( currency );
        SetPersonCount( minPersonCount, maxPersonCount );
        SetTotalRooms( totalRooms );
        SetServices( services );
        SetAmenities( amenities );
    }

    private RoomType()
    {
    }

    public void SetName( string name )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ArgumentException( $"'{nameof( name )}' cannot be null or whitespace.", nameof( name ) );
        }

        Name = name;
    }

    public void SetDailyPrice( decimal dailyPrice )
    {
        if ( dailyPrice <= 0 )
        {
            throw new ArgumentException( "Daily price must be greater than zero.", nameof( dailyPrice ) );
        }

        DailyPrice = dailyPrice;
    }

    public void SetCurrency( string currency )
    {
        if ( string.IsNullOrWhiteSpace( currency ) )
        {
            throw new ArgumentException( $"'{nameof( currency )}' cannot be null or whitespace.", nameof( currency ) );
        }

        Currency = currency;
    }

    public void SetPersonCount( int minPersonCount, int maxPersonCount )
    {
        if ( minPersonCount <= 0 )
        {
            throw new ArgumentException( "Min person count must be greater than zero.", nameof( minPersonCount ) );
        }

        if ( maxPersonCount < minPersonCount )
        {
            throw new ArgumentException( "Max person count cannot be less than min person count.", nameof( maxPersonCount ) );
        }

        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
    }

    public void SetTotalRooms( int totalRooms )
    {
        if ( totalRooms <= 0 )
        {
            throw new ArgumentException( "Total rooms must be greater than zero.", nameof( totalRooms ) );
        }

        TotalRooms = totalRooms;
    }

    public void SetServices( string services )
    {
        Services = services;
    }

    public void SetAmenities( string amenities )
    {
        Amenities = amenities;
    }

    public void CopyFrom( RoomType other )
    {
        SetName( other.Name );
        SetDailyPrice( other.DailyPrice );
        SetCurrency( other.Currency );
        SetPersonCount( other.MinPersonCount, other.MaxPersonCount );
        SetTotalRooms( other.TotalRooms );
        SetServices( other.Services );
        SetAmenities( other.Amenities );
    }
}