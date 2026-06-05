using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    IReadOnlyList<RoomType> GetAllRoomTypes();

    IReadOnlyList<RoomType> GetRoomTypesByPropertyId( int propertyId );

    RoomType GetRoomTypeById( int id );

    void Save( RoomType roomType );

    void Update( RoomType roomType );

    void Delete( int id );
}