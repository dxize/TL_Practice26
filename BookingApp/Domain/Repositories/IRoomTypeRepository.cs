using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    IReadOnlyList<RoomType> GetAll();

    IReadOnlyList<RoomType> GetRoomTypesByPropertyId( int propertyId, int? guests = null, decimal? maxPrice = null );

    RoomType GetById( int id );

    void Save( RoomType roomType );

    void Update( RoomType roomType );

    void Delete( RoomType roomType );
}
