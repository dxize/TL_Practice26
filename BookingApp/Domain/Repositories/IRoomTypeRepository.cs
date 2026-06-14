using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    IReadOnlyList<RoomType> GetAll();

    IReadOnlyList<RoomType> GetRoomTypesByPropertyId( int propertyId );

    RoomType GetById( int id );

    void Save( RoomType roomType );

    void Update( RoomType roomType );

    void Delete( int id );
}