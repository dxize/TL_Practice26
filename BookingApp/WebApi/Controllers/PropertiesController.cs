using Application.Services;
using Domain.Entities;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly PropertyService _propertyService;

    public PropertiesController( PropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet( "" )]
    public IActionResult GetProperties()
    {
        IReadOnlyList<Property> properties = _propertyService.GetAll();

        List<PropertyResponse> response = properties.Select( MapToResponse ).ToList();

        return Ok( response );
    }

    [HttpGet( "{id:int}" )]
    public IActionResult GetProperty( [FromRoute] int id )
    {
        try
        {
            Property property = _propertyService.GetById( id );

            PropertyResponse response = MapToResponse( property );

            return Ok( response );
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpPost( "" )]
    public IActionResult CreateProperty( [FromBody] CreatePropertyRequest request )
    {
        _propertyService.Create(
            request.Name,
            request.Country,
            request.City,
            request.Address,
            request.Latitude,
            request.Longitude );

        return Ok();
    }

    [HttpPut( "{id:int}" )]
    public IActionResult ModifyProperty( [FromRoute] int id, [FromBody] ModifyPropertyRequest request )
    {
        try
        {
            _propertyService.Update(
                id,
                request.Name,
                request.Country,
                request.City,
                request.Address,
                request.Latitude,
                request.Longitude );

            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpDelete( "{id:int}" )]
    public IActionResult DeleteProperty( [FromRoute] int id )
    {
        try
        {
            _propertyService.Delete( id );
            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    private static PropertyResponse MapToResponse( Property property )
    {
        return new PropertyResponse
        {
            Id = property.Id,
            Name = property.Name,
            Country = property.Country,
            City = property.City,
            Address = property.Address,
            Latitude = property.Latitude,
            Longitude = property.Longitude
        };
    }
}
