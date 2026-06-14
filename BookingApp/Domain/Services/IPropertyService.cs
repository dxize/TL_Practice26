using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    IReadOnlyList<Property> GetAll();

    Property GetById( int id );

    void Create( Property property );

    void Update( int id, Action<Property> updateAction );

    void Delete( int id );
}
