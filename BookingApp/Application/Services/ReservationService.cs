using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class ReservationService
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
        return _reservationRepository.GetAll( propertyId, guestName, dateFrom, dateTo );
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

    public Reservation Create(
        int propertyId,
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate,
        TimeSpan arrivalTime,
        TimeSpan departureTime,
        string guestName,
        string guestPhoneNumber,
        int guests )
    {
        Property property = _propertyRepository.GetById( propertyId );
        if ( property is null )
        {
            throw new ArgumentException( "Объект размещения не найден." );
        }

        RoomType roomType = _roomTypeRepository.GetById( roomTypeId );
        if ( roomType is null )
        {
            throw new ArgumentException( "Категория номера не найдена." );
        }

        if ( roomType.PropertyId != propertyId )
        {
            throw new ArgumentException( "Категория номера не принадлежит указанному объекту размещения." );
        }

        if ( guests < roomType.MinPersonCount || guests > roomType.MaxPersonCount )
        {
            throw new ArgumentException(
                $"Количество гостей должно быть от {roomType.MinPersonCount} до {roomType.MaxPersonCount}." );
        }

        IReadOnlyList<Reservation> overlappingReservations =
            _reservationRepository.GetActiveReservationsByRoomTypeAndDates(
                roomTypeId, arrivalDate, departureDate );

        if ( overlappingReservations.Count >= roomType.TotalRooms )
        {
            throw new ArgumentException( "Нет доступных номеров на указанный период." );
        }

        int nights = ( departureDate - arrivalDate ).Days;
        decimal total = roomType.DailyPrice * nights;
        string currency = roomType.Currency;

        Reservation reservation = new(
            propertyId,
            roomTypeId,
            arrivalDate,
            departureDate,
            arrivalTime,
            departureTime,
            guestName,
            guestPhoneNumber,
            guests,
            total,
            currency );

        _reservationRepository.Save( reservation );
        return reservation;
    }

    public void Cancel( int id )
    {
        Reservation reservation = GetById( id );
        reservation.Cancel();
        _reservationRepository.Update( reservation );
    }
}
