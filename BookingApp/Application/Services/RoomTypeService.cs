using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;

    public RoomTypeService(
        IRoomTypeRepository roomTypeRepository,
        IPropertyRepository propertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
    }

    public IReadOnlyList<RoomType> GetByPropertyId( int propertyId )
    {
        Property property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with id {propertyId} not found." );
        }

        return _roomTypeRepository.GetRoomTypesByPropertyId( propertyId );
    }

    public RoomType GetById( int id )
    {
        RoomType roomType = _roomTypeRepository.GetById( id );
        if ( roomType is null )
        {
            throw new KeyNotFoundException( $"RoomType with id {id} not found." );
        }

        return roomType;
    }

    public void Create( int propertyId, RoomType roomType )
    {
        Property property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with id {propertyId} not found." );
        }

        _roomTypeRepository.Save( roomType );
    }

    public void Update( int id, Action<RoomType> updateAction )
    {
        RoomType roomType = GetById( id );
        updateAction( roomType );
        _roomTypeRepository.Update( roomType );
    }

    public void Delete( int id )
    {
        RoomType roomType = GetById( id );
        _roomTypeRepository.Delete( roomType );
    }
}
