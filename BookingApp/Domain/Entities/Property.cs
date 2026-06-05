namespace Domain.Entities;

public class Property
{
    public int Id { get; private init; }

    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public List<RoomType> RoomTypes { get; private init; } = new();
    public List<Reservation> Reservations { get; private init; } = new();

    public Property(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude )
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Property(
        int id,
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude )
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    private Property()
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

    public void SetCountry( string country )
    {
        if ( string.IsNullOrWhiteSpace( country ) )
        {
            throw new ArgumentException( $"'{nameof( country )}' cannot be null or whitespace.", nameof( country ) );
        }

        Country = country;
    }

    public void SetCity( string city )
    {
        if ( string.IsNullOrWhiteSpace( city ) )
        {
            throw new ArgumentException( $"'{nameof( city )}' cannot be null or whitespace.", nameof( city ) );
        }

        City = city;
    }

    public void SetAddress( string address )
    {
        if ( string.IsNullOrWhiteSpace( address ) )
        {
            throw new ArgumentException( $"'{nameof( address )}' cannot be null or whitespace.", nameof( address ) );
        }

        Address = address;
    }

    public void SetCoordinates( double latitude, double longitude )
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public void CopyFrom( Property other )
    {
        SetName( other.Name );
        SetCountry( other.Country );
        SetCity( other.City );
        SetAddress( other.Address );
        SetCoordinates( other.Latitude, other.Longitude );
    }
}