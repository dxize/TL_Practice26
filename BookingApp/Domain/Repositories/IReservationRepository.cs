using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    IReadOnlyList<Reservation> GetAll(
        int? propertyId = null,
        string? guestName = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null );

    Reservation? GetById( int id );

    IReadOnlyList<Reservation> GetActiveReservationsByRoomTypeAndDates(
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate );

    void Save( Reservation reservation );

    void Update( Reservation reservation );

    void Delete( Reservation reservation );
}
