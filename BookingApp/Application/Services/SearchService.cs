using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class SearchService : ISearchService
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
        IReadOnlyList<Property> properties = _propertyRepository.GetAll();
        List<Property> filteredProperties = properties
            .Where( property => property.City.Equals( city, StringComparison.OrdinalIgnoreCase ) )
            .ToList();

        List<SearchResult> results = new();

        foreach ( Property property in filteredProperties )
        {
            IReadOnlyList<RoomType> roomTypes = _roomTypeRepository.GetRoomTypesByPropertyId( property.Id );

            List<RoomType> availableRoomTypes = new();

            foreach ( RoomType roomType in roomTypes )
            {
                if ( guests < roomType.MinPersonCount || guests > roomType.MaxPersonCount )
                {
                    continue;
                }

                if ( maxPrice.HasValue && roomType.DailyPrice > maxPrice.Value )
                {
                    continue;
                }

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
