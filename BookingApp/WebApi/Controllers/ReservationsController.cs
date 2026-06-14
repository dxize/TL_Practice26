using Domain.Entities;
using Domain.Services;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpGet( "" )]
    public IActionResult GetReservations(
        [FromQuery] int? propertyId,
        [FromQuery] string guestName,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo )
    {
        IReadOnlyList<Reservation> reservations = _reservationService.GetAll(
            propertyId, guestName, dateFrom, dateTo );

        List<ReservationResponse> response = reservations.Select( r => new ReservationResponse
        {
            Id = r.Id,
            PropertyId = r.PropertyId,
            RoomTypeId = r.RoomTypeId,
            ArrivalDate = r.ArrivalDate,
            DepartureDate = r.DepartureDate,
            ArrivalTime = r.ArrivalTime,
            DepartureTime = r.DepartureTime,
            GuestName = r.GuestName,
            GuestPhoneNumber = r.GuestPhoneNumber,
            Guests = r.Guests,
            Total = r.Total,
            Currency = r.Currency,
            IsCanceled = r.IsCanceled
        } ).ToList();

        return Ok( response );
    }

    [HttpGet( "{id:int}" )]
    public IActionResult GetReservation( [FromRoute] int id )
    {
        try
        {
            Reservation reservation = _reservationService.GetById( id );

            ReservationResponse response = new()
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
        if ( request.ArrivalDate >= request.DepartureDate )
        {
            return BadRequest( "Дата заезда должна быть раньше даты выезда." );
        }

        try
        {
            Reservation reservation = new(
                request.PropertyId,
                request.RoomTypeId,
                request.ArrivalDate,
                request.DepartureDate,
                request.ArrivalTime,
                request.DepartureTime,
                request.GuestName,
                request.GuestPhoneNumber,
                request.Guests,
                0,
                string.Empty );

            _reservationService.Create( reservation );

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
}
