using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    IReadOnlyList<Property> GetAll();

    Property GetById( int id );

    void Save( Property property );

    void Update( Property property );

    void Delete( int id );
}