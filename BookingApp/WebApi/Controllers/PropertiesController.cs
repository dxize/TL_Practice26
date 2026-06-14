using Domain.Entities;
using Domain.Repositories;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertiesController( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    [HttpGet( "" )]
    public IActionResult GetProperties()
    {
        IReadOnlyList<Property> properties = _propertyRepository.GetAll();

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
        Property property = _propertyRepository.GetById( id );
        if ( property is null )
        {
            return NotFound();
        }

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

        _propertyRepository.Save( property );

        return Ok();
    }

    [HttpPut( "{id:int}" )]
    public IActionResult ModifyProperty( [FromRoute] int id, [FromBody] ModifyPropertyRequest request )
    {
        Property existingProperty = _propertyRepository.GetById( id );
        if ( existingProperty is null )
        {
            return NotFound();
        }

        existingProperty.SetName( request.Name );
        existingProperty.SetCountry( request.Country );
        existingProperty.SetCity( request.City );
        existingProperty.SetAddress( request.Address );
        existingProperty.SetCoordinates( request.Latitude, request.Longitude );

        _propertyRepository.Update( existingProperty );

        return Ok();
    }

    [HttpDelete( "{id:int}" )]
    public IActionResult DeleteProperty( [FromRoute] int id )
    {
        _propertyRepository.Delete( id );

        return Ok();
    }
}
