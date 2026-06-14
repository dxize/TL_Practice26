using Domain.Entities;
using Domain.Repositories;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/search" )]
public class SearchController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public SearchController(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    [HttpGet( "" )]
    public IActionResult Search(
        [FromQuery] string city,
        [FromQuery] DateTime arrivalDate,
        [FromQuery] DateTime departureDate,
        [FromQuery] int guests,
        [FromQuery] decimal? maxPrice )
    {
        if ( arrivalDate >= departureDate )
        {
            return BadRequest( "Дата заезда должна быть раньше даты выезда." );
        }

        IReadOnlyList<Property> properties = _propertyRepository.GetAll();
        List<Property> filteredProperties = properties
            .Where( property => property.City.Equals( city, StringComparison.OrdinalIgnoreCase ) )
            .ToList();

        List<SearchResultResponse> results = new();

        foreach ( Property property in filteredProperties )
        {
            IReadOnlyList<RoomType> roomTypes = _roomTypeRepository.GetRoomTypesByPropertyId( property.Id );

            List<RoomTypeResponse> availableRoomTypes = new();

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

                int occupiedRooms = overlappingReservations.Count;
                if ( occupiedRooms >= roomType.TotalRooms )
                {
                    continue;
                }

                availableRoomTypes.Add( new RoomTypeResponse
                {
                    Id = roomType.Id,
                    PropertyId = roomType.PropertyId,
                    Name = roomType.Name,
                    DailyPrice = roomType.DailyPrice,
                    Currency = roomType.Currency,
                    MinPersonCount = roomType.MinPersonCount,
                    MaxPersonCount = roomType.MaxPersonCount,
                    TotalRooms = roomType.TotalRooms,
                    Services = roomType.Services,
                    Amenities = roomType.Amenities
                } );
            }

            if ( availableRoomTypes.Count > 0 )
            {
                results.Add( new SearchResultResponse
                {
                    Property = new PropertyResponse
                    {
                        Id = property.Id,
                        Name = property.Name,
                        Country = property.Country,
                        City = property.City,
                        Address = property.Address,
                        Latitude = property.Latitude,
                        Longitude = property.Longitude
                    },
                    AvailableRoomTypes = availableRoomTypes
                } );
            }
        }

        return Ok( results );
    }
}
