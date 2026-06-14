using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    IReadOnlyList<RoomType> GetByPropertyId( int propertyId );

    RoomType GetById( int id );

    void Create( int propertyId, RoomType roomType );

    void Update( int id, Action<RoomType> updateAction );

    void Delete( int id );
}
