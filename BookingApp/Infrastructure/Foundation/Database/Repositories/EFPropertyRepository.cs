using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Database.Repositories;

public class EfPropertyRepository : IPropertyRepository
{
    private readonly BookingDbContext _dbContext;

    public EfPropertyRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Property> GetAll()
    {
        return _dbContext.Set<Property>().ToList();
    }

    public Property GetById( int id )
    {
        return _dbContext.Set<Property>().FirstOrDefault( property => property.Id == id );
    }

    public void Save( Property property )
    {
        _dbContext.Set<Property>().Add( property );
        _dbContext.SaveChanges();
    }

    public void Update( Property property )
    {
        Property existingProperty = GetById( property.Id );
        existingProperty.CopyFrom( property );
        _dbContext.SaveChanges();
    }

    public void Delete( int id )
    {
        Property existingProperty = GetById( id );
        _dbContext.Set<Property>().Remove( existingProperty );
        _dbContext.SaveChanges();
    }
}