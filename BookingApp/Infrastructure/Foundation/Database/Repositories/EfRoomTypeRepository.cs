using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Database.Repositories;

public class EfRoomTypeRepository : IRoomTypeRepository
{
    private readonly BookingDbContext _dbContext;

    public EfRoomTypeRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<RoomType> GetAll()
    {
        return _dbContext.Set<RoomType>().ToList();
    }

    public IReadOnlyList<RoomType> GetRoomTypesByPropertyId( int propertyId, int? guests = null, decimal? maxPrice = null )
    {
        IQueryable<RoomType> query = _dbContext.Set<RoomType>()
            .Where( roomType => roomType.PropertyId == propertyId )
            .AsQueryable();

        if ( guests.HasValue )
        {
            query = query.Where( rt => guests.Value >= rt.MinPersonCount && guests.Value <= rt.MaxPersonCount );
        }

        if ( maxPrice.HasValue )
        {
            query = query.Where( rt => rt.DailyPrice <= maxPrice.Value );
        }

        return query.ToList();
    }

    public RoomType? GetById( int id )
    {
        return _dbContext.Set<RoomType>().FirstOrDefault( roomType => roomType.Id == id );
    }

    public void Save( RoomType roomType )
    {
        _dbContext.Set<RoomType>().Add( roomType );
        _dbContext.SaveChanges();
    }

    public void Update( RoomType roomType )
    {
        _dbContext.Set<RoomType>().Update( roomType );
        _dbContext.SaveChanges();
    }

    public void Delete( RoomType roomType )
    {
        _dbContext.Set<RoomType>().Remove( roomType );
        _dbContext.SaveChanges();
    }
}
