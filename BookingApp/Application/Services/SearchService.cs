using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class SearchService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public SearchService(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public IReadOnlyList<SearchResult> Search(
        string city,
        DateTime arrivalDate,
        DateTime departureDate,
        int guests,
        decimal? maxPrice )
    {
        IReadOnlyList<Property> properties = _propertyRepository.GetAll( city );

        List<SearchResult> results = new();

        foreach ( Property property in properties )
        {
            IReadOnlyList<RoomType> roomTypes = _roomTypeRepository.GetRoomTypesByPropertyId( property.Id, guests, maxPrice );

            List<RoomType> availableRoomTypes = new();

            foreach ( RoomType roomType in roomTypes )
            {
                IReadOnlyList<Reservation> overlappingReservations =
                    _reservationRepository.GetActiveReservationsByRoomTypeAndDates(
                        roomType.Id, arrivalDate, departureDate );

                if ( overlappingReservations.Count >= roomType.TotalRooms )
                {
                    continue;
                }

                availableRoomTypes.Add( roomType );
            }

            if ( availableRoomTypes.Count > 0 )
            {
                results.Add( new SearchResult
                {
                    Property = property,
                    AvailableRoomTypes = availableRoomTypes
                } );
            }
        }

        return results;
    }
}
