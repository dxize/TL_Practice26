using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    IReadOnlyList<Reservation> GetAllReservations();

    Reservation GetReservationById( int id );

    IReadOnlyList<Reservation> GetActiveReservationsByRoomTypeAndDates(
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate );

    void Save( Reservation reservation );

    void Update( Reservation reservation );

    void Delete( int id );
}