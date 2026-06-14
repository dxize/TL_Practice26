using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository )
    {
        _reservationRepository = reservationRepository;
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    public IReadOnlyList<Reservation> GetAll(
        int? propertyId,
        string guestName,
        DateTime? dateFrom,
        DateTime? dateTo )
    {
        IReadOnlyList<Reservation> reservations = _reservationRepository.GetAll();

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

        return filtered.ToList();
    }

    public Reservation GetById( int id )
    {
        Reservation reservation = _reservationRepository.GetById( id );
        if ( reservation is null )
        {
            throw new KeyNotFoundException( $"Reservation with id {id} not found." );
        }

        return reservation;
    }

    public void Create( Reservation reservation )
    {
        Property property = _propertyRepository.GetById( reservation.PropertyId );
        if ( property is null )
        {
            throw new ArgumentException( "Объект размещения не найден." );
        }

        RoomType roomType = _roomTypeRepository.GetById( reservation.RoomTypeId );
        if ( roomType is null )
        {
            throw new ArgumentException( "Категория номера не найдена." );
        }

        if ( roomType.PropertyId != reservation.PropertyId )
        {
            throw new ArgumentException( "Категория номера не принадлежит указанному объекту размещения." );
        }

        if ( reservation.Guests < roomType.MinPersonCount || reservation.Guests > roomType.MaxPersonCount )
        {
            throw new ArgumentException(
                $"Количество гостей должно быть от {roomType.MinPersonCount} до {roomType.MaxPersonCount}." );
        }

        IReadOnlyList<Reservation> overlappingReservations =
            _reservationRepository.GetActiveReservationsByRoomTypeAndDates(
                reservation.RoomTypeId, reservation.ArrivalDate, reservation.DepartureDate );

        if ( overlappingReservations.Count >= roomType.TotalRooms )
        {
            throw new ArgumentException( "Нет доступных номеров на указанный период." );
        }

        _reservationRepository.Save( reservation );
    }

    public void Cancel( int id )
    {
        Reservation reservation = GetById( id );
        reservation.Cancel();
        _reservationRepository.Update( reservation );
    }
}
