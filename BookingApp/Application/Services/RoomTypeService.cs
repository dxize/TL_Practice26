using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class RoomTypeService
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
        Property? property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with id {propertyId} not found." );
        }

        return _roomTypeRepository.GetRoomTypesByPropertyId( propertyId );
    }

    public RoomType GetById( int id )
    {
        RoomType? roomType = _roomTypeRepository.GetById( id );
        if ( roomType is null )
        {
            throw new KeyNotFoundException( $"RoomType with id {id} not found." );
        }

        return roomType;
    }

    public RoomType Create( int propertyId, string name, decimal dailyPrice, string currency, int minPersonCount, int maxPersonCount, int totalRooms, string services, string amenities )
    {
        Property? property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            throw new KeyNotFoundException( $"Property with id {propertyId} not found." );
        }

        RoomType roomType = new( propertyId, name, dailyPrice, currency, minPersonCount, maxPersonCount, totalRooms, services, amenities );
        _roomTypeRepository.Save( roomType );

        return roomType;
    }

    public void Update( int id, string name, decimal dailyPrice, string currency, int minPersonCount, int maxPersonCount, int totalRooms, string services, string amenities )
    {
        RoomType roomType = GetById( id );
        roomType.SetName( name );
        roomType.SetDailyPrice( dailyPrice );
        roomType.SetCurrency( currency );
        roomType.SetPersonCount( minPersonCount, maxPersonCount );
        roomType.SetTotalRooms( totalRooms );
        roomType.SetServices( services );
        roomType.SetAmenities( amenities );
        _roomTypeRepository.Update( roomType );
    }

    public void Delete( int id )
    {
        RoomType roomType = GetById( id );
        _roomTypeRepository.Delete( roomType );
    }
}
