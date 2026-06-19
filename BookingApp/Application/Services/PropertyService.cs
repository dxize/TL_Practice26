using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class PropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public IReadOnlyList<Property> GetAll()
    {
        return _propertyRepository.GetAll();
    }

    public Property GetById( int id )
    {
        Property property = _propertyRepository.GetById( id );
        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with id {id} not found." );
        }

        return property;
    }

    public Property Create( string name, string country, string city, string address, double latitude, double longitude )
    {
        Property property = new( name, country, city, address, latitude, longitude );
        _propertyRepository.Save( property );
        return property;
    }

    public void Update( int id, string name, string country, string city, string address, double latitude, double longitude )
    {
        Property property = GetById( id );
        property.SetName( name );
        property.SetCountry( country );
        property.SetCity( city );
        property.SetAddress( address );
        property.SetCoordinates( latitude, longitude );
        _propertyRepository.Update( property );
    }

    public void Delete( int id )
    {
        Property property = GetById( id );
        _propertyRepository.Delete( property );
    }
}
