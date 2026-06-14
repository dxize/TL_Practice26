using Domain.Entities;
using Domain.Services;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet( "" )]
    public IActionResult GetProperties()
    {
        IReadOnlyList<Property> properties = _propertyService.GetAll();

        List<PropertyResponse> response = properties.Select( property => new PropertyResponse
        {
            Id = property.Id,
            Name = property.Name,
            Country = property.Country,
            City = property.City,
            Address = property.Address,
            Latitude = property.Latitude,
            Longitude = property.Longitude
        } ).ToList();

        return Ok( response );
    }

    [HttpGet( "{id:int}" )]
    public IActionResult GetProperty( [FromRoute] int id )
    {
        try
        {
            Property property = _propertyService.GetById( id );

            PropertyResponse response = new()
            {
                Id = property.Id,
                Name = property.Name,
                Country = property.Country,
                City = property.City,
                Address = property.Address,
                Latitude = property.Latitude,
                Longitude = property.Longitude
            };

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
        Property property = new(
            request.Name,
            request.Country,
            request.City,
            request.Address,
            request.Latitude,
            request.Longitude );

        _propertyService.Create( property );

        return Ok();
    }

    [HttpPut( "{id:int}" )]
    public IActionResult ModifyProperty( [FromRoute] int id, [FromBody] ModifyPropertyRequest request )
    {
        try
        {
            _propertyService.Update( id, property =>
            {
                property.SetName( request.Name );
                property.SetCountry( request.Country );
                property.SetCity( request.City );
                property.SetAddress( request.Address );
                property.SetCoordinates( request.Latitude, request.Longitude );
            } );

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
}
