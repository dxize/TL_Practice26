using Domain.Entities;
using Domain.Repositories;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api" )]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;

    public RoomTypesController( IRoomTypeRepository roomTypeRepository, IPropertyRepository propertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
    }

    [HttpGet( "properties/{propertyId:int}/roomtypes" )]
    public IActionResult GetRoomTypesByProperty( [FromRoute] int propertyId )
    {
        Property property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            return NotFound();
        }

        IReadOnlyList<RoomType> roomTypes = _roomTypeRepository.GetRoomTypesByPropertyId( propertyId );

        List<RoomTypeResponse> response = roomTypes.Select( roomType => new RoomTypeResponse
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
        } ).ToList();

        return Ok( response );
    }

    [HttpGet( "roomtypes/{id:int}" )]
    public IActionResult GetRoomType( [FromRoute] int id )
    {
        RoomType roomType = _roomTypeRepository.GetById( id );
        if ( roomType is null )
        {
            return NotFound();
        }

        RoomTypeResponse response = new()
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
        };

        return Ok( response );
    }

    [HttpPost( "properties/{propertyId:int}/roomtypes" )]
    public IActionResult CreateRoomType( [FromRoute] int propertyId, [FromBody] CreateRoomTypeRequest request )
    {
        Property property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            return NotFound();
        }

        RoomType roomType = new(
            propertyId,
            request.Name,
            request.DailyPrice,
            request.Currency,
            request.MinPersonCount,
            request.MaxPersonCount,
            request.TotalRooms,
            request.Services,
            request.Amenities );

        _roomTypeRepository.Save( roomType );

        return Ok();
    }

    [HttpPut( "roomtypes/{id:int}" )]
    public IActionResult ModifyRoomType( [FromRoute] int id, [FromBody] ModifyRoomTypeRequest request )
    {
        RoomType existingRoomType = _roomTypeRepository.GetById( id );
        if ( existingRoomType is null )
        {
            return NotFound();
        }

        existingRoomType.SetName( request.Name );
        existingRoomType.SetDailyPrice( request.DailyPrice );
        existingRoomType.SetCurrency( request.Currency );
        existingRoomType.SetPersonCount( request.MinPersonCount, request.MaxPersonCount );
        existingRoomType.SetTotalRooms( request.TotalRooms );
        existingRoomType.SetServices( request.Services );
        existingRoomType.SetAmenities( request.Amenities );

        _roomTypeRepository.Update( existingRoomType );

        return Ok();
    }

    [HttpDelete( "roomtypes/{id:int}" )]
    public IActionResult DeleteRoomType( [FromRoute] int id )
    {
        _roomTypeRepository.Delete( id );

        return Ok();
    }
}
