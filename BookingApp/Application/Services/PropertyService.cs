using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class PropertyService : IPropertyService
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

    public void Create( Property property )
    {
        _propertyRepository.Save( property );
    }

    public void Update( int id, Action<Property> updateAction )
    {
        Property property = GetById( id );
        updateAction( property );
        _propertyRepository.Update( property );
    }

    public void Delete( int id )
    {
        Property property = GetById( id );
        _propertyRepository.Delete( property );
    }
}
