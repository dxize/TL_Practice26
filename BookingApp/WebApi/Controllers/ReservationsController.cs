using Application.Services;
using Domain.Entities;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationsController( ReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpGet( "" )]
    public IActionResult GetReservations(
        [FromQuery] int? propertyId = null,
        [FromQuery] string guestName = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null )
    {
        IReadOnlyList<Reservation> reservations = _reservationService.GetAll(
            propertyId, guestName, dateFrom, dateTo );

        List<ReservationResponse> response = reservations.Select( MapToResponse ).ToList();

        return Ok( response );
    }

    [HttpGet( "{id:int}" )]
    public IActionResult GetReservation( [FromRoute] int id )
    {
        try
        {
            Reservation reservation = _reservationService.GetById( id );

            ReservationResponse response = MapToResponse( reservation );

            return Ok( response );
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    [HttpPost( "" )]
    public IActionResult CreateReservation( [FromBody] CreateReservationRequest request )
    {
        try
        {
            _reservationService.Create(
                request.PropertyId,
                request.RoomTypeId,
                request.ArrivalDate,
                request.DepartureDate,
                request.ArrivalTime,
                request.DepartureTime,
                request.GuestName,
                request.GuestPhoneNumber,
                request.Guests );

            return Ok();
        }
        catch ( ArgumentException ex )
        {
            return BadRequest( ex.Message );
        }
    }

    [HttpDelete( "{id:int}" )]
    public IActionResult CancelReservation( [FromRoute] int id )
    {
        try
        {
            _reservationService.Cancel( id );
            return Ok();
        }
        catch ( KeyNotFoundException )
        {
            return NotFound();
        }
    }

    private static ReservationResponse MapToResponse( Reservation reservation )
    {
        return new ReservationResponse
        {
            Id = reservation.Id,
            PropertyId = reservation.PropertyId,
            RoomTypeId = reservation.RoomTypeId,
            ArrivalDate = reservation.ArrivalDate,
            DepartureDate = reservation.DepartureDate,
            ArrivalTime = reservation.ArrivalTime,
            DepartureTime = reservation.DepartureTime,
            GuestName = reservation.GuestName,
            GuestPhoneNumber = reservation.GuestPhoneNumber,
            Guests = reservation.Guests,
            Total = reservation.Total,
            Currency = reservation.Currency,
            IsCanceled = reservation.IsCanceled
        };
    }
}
