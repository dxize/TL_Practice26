using Domain.Entities;
using Domain.Services;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/search" )]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController( ISearchService searchService )
    {
        _searchService = searchService;
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

        IReadOnlyList<SearchResult> searchResults = _searchService.Search(
            city, arrivalDate, departureDate, guests, maxPrice );

        List<SearchResultResponse> response = searchResults.Select( result => new SearchResultResponse
        {
            Property = new PropertyResponse
            {
                Id = result.Property.Id,
                Name = result.Property.Name,
                Country = result.Property.Country,
                City = result.Property.City,
                Address = result.Property.Address,
                Latitude = result.Property.Latitude,
                Longitude = result.Property.Longitude
            },
            AvailableRoomTypes = result.AvailableRoomTypes.Select( roomType => new RoomTypeResponse
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
            } ).ToList()
        } ).ToList();

        return Ok( response );
    }
}
