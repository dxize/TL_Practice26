using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    IReadOnlyList<Property> GetAllProperties();

    Property GetPropertyById( int id );

    void Save( Property property );

    void Update( Property property );

    void Delete( int id );
}