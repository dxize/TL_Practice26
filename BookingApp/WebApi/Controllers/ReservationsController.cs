using Domain.Entities;
using Domain.Repositories;
using WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public ReservationsController(
        IReservationRepository reservationRepository,
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository )
    {
        _reservationRepository = reservationRepository;
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    [HttpGet( "" )]
    public IActionResult GetReservations(
        [FromQuery] int? propertyId,
        [FromQuery] string guestName,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo )
    {
        IReadOnlyList<Reservation> reservations = _reservationRepository.GetAllReservations();

        IEnumerable<Reservation> filtered = reservations;

        if ( propertyId.HasValue )
        {
            filtered = filtered.Where( r => r.PropertyId == propertyId.Value );
        }

        if ( !string.IsNullOrWhiteSpace( guestName ) )
        {
            filtered = filtered.Where( r => r.GuestName.Contains( guestName, StringComparison.OrdinalIgnoreCase ) );
        }

        if ( dateFrom.HasValue )
        {
            filtered = filtered.Where( r => r.DepartureDate > dateFrom.Value );
        }

        if ( dateTo.HasValue )
        {
            filtered = filtered.Where( r => r.ArrivalDate < dateTo.Value );
        }

        List<ReservationResponse> response = filtered.Select( r => new ReservationResponse
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
        Reservation reservation = _reservationRepository.GetReservationById( id );
        if ( reservation is null )
        {
            return NotFound();
        }

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

    [HttpPost( "" )]
    public IActionResult CreateReservation( [FromBody] CreateReservationRequest request )
    {
        if ( request.ArrivalDate >= request.DepartureDate )
        {
            return BadRequest( "Дата заезда должна быть раньше даты выезда." );
        }

        Property property = _propertyRepository.GetPropertyById( request.PropertyId );
        if ( property is null )
        {
            return BadRequest( "Объект размещения не найден." );
        }

        RoomType roomType = _roomTypeRepository.GetRoomTypeById( request.RoomTypeId );
        if ( roomType is null )
        {
            return BadRequest( "Категория номера не найдена." );
        }

        if ( roomType.PropertyId != request.PropertyId )
        {
            return BadRequest( "Категория номера не принадлежит указанному объекту размещения." );
        }

        if ( request.Guests < roomType.MinPersonCount || request.Guests > roomType.MaxPersonCount )
        {
            return BadRequest( $"Количество гостей должно быть от {roomType.MinPersonCount} до {roomType.MaxPersonCount}." );
        }

        IReadOnlyList<Reservation> overlappingReservations =
            _reservationRepository.GetActiveReservationsByRoomTypeAndDates(
                request.RoomTypeId, request.ArrivalDate, request.DepartureDate );

        if ( overlappingReservations.Count >= roomType.TotalRooms )
        {
            return BadRequest( "Нет доступных номеров на указанный период." );
        }

        int nights = ( request.DepartureDate.Date - request.ArrivalDate.Date ).Days;
        decimal total = roomType.DailyPrice * nights;

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
            total,
            roomType.Currency );

        _reservationRepository.Save( reservation );

        return Ok();
    }

    [HttpDelete( "{id:int}" )]
    public IActionResult CancelReservation( [FromRoute] int id )
    {
        Reservation reservation = _reservationRepository.GetReservationById( id );
        if ( reservation is null )
        {
            return NotFound();
        }

        reservation.Cancel();
        _reservationRepository.Update( reservation );

        return Ok();
    }
}
