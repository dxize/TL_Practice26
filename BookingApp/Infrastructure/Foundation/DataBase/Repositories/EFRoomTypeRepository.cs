using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomTypeRepository : IRoomTypeRepository
{
    private readonly BookingDbContext _dbContext;

    public EFRoomTypeRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<RoomType> GetAllRoomTypes()
    {
        return _dbContext.Set<RoomType>().ToList();
    }

    public IReadOnlyList<RoomType> GetRoomTypesByPropertyId( int propertyId )
    {
        return _dbContext.Set<RoomType>()
            .Where( roomType => roomType.PropertyId == propertyId )
            .ToList();
    }

    public RoomType GetRoomTypeById( int id )
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
        RoomType existingRoomType = GetRoomTypeById( roomType.Id );
        existingRoomType.CopyFrom( roomType );
        _dbContext.SaveChanges();
    }

    public void Delete( int id )
    {
        RoomType existingRoomType = GetRoomTypeById( id );
        _dbContext.Set<RoomType>().Remove( existingRoomType );
        _dbContext.SaveChanges();
    }
}