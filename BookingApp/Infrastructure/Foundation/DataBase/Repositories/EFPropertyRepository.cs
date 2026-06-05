using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFPropertyRepository : IPropertyRepository
{
    private readonly BookingDbContext _dbContext;

    public EFPropertyRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Property> GetAllProperties()
    {
        return _dbContext.Set<Property>().ToList();
    }

    public Property GetPropertyById( int id )
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
        Property existingProperty = GetPropertyById( property.Id );
        existingProperty.CopyFrom( property );
        _dbContext.SaveChanges();
    }

    public void Delete( int id )
    {
        Property existingProperty = GetPropertyById( id );
        _dbContext.Set<Property>().Remove( existingProperty );
        _dbContext.SaveChanges();
    }
}