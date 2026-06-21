using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    IReadOnlyList<Property> GetAll( string? city = null );

    Property? GetById( int id );

    void Save( Property property );

    void Update( Property property );

    void Delete( Property property );
}
