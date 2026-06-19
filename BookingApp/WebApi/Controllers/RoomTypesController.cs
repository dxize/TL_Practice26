using Application.Services;
using Domain.Entities;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api" )]
public class RoomTypesController : ControllerBase
{
    private readonly RoomTypeService _roomTypeService;

    public RoomTypesController( RoomTypeService roomTypeService )
    {
        _roomTypeService = roomTypeService;
    }

    [HttpGet( "properties/{propertyId:int}/roomtypes" )]
    public IActionResult GetRoomTypesByProperty( [FromRoute] int propertyId )
    {
        try
        {
            IReadOnlyList<RoomType> roomTypes = _roomTypeService.GetByPropertyId( propertyId );

            List<RoomTypeResponse> response = roomTypes.Select( MapToResponse ).ToList();

            return Ok( response );
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpGet( "roomtypes/{id:int}" )]
    public IActionResult GetRoomType( [FromRoute] int id )
    {
        try
        {
            RoomType roomType = _roomTypeService.GetById( id );

            RoomTypeResponse response = MapToResponse( roomType );

            return Ok( response );
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpPost( "properties/{propertyId:int}/roomtypes" )]
    public IActionResult CreateRoomType( [FromRoute] int propertyId, [FromBody] CreateRoomTypeRequest request )
    {
        try
        {
            _roomTypeService.Create(
                propertyId,
                request.Name,
                request.DailyPrice,
                request.Currency,
                request.MinPersonCount,
                request.MaxPersonCount,
                request.TotalRooms,
                request.Services,
                request.Amenities );

            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpPut( "roomtypes/{id:int}" )]
    public IActionResult ModifyRoomType( [FromRoute] int id, [FromBody] ModifyRoomTypeRequest request )
    {
        try
        {
            _roomTypeService.Update(
                id,
                request.Name,
                request.DailyPrice,
                request.Currency,
                request.MinPersonCount,
                request.MaxPersonCount,
                request.TotalRooms,
                request.Services,
                request.Amenities );

            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpDelete( "roomtypes/{id:int}" )]
    public IActionResult DeleteRoomType( [FromRoute] int id )
    {
        try
        {
            _roomTypeService.Delete( id );
            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    private static RoomTypeResponse MapToResponse( RoomType roomType )
    {
        return new RoomTypeResponse
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
    }
}
