using Domain.Entities;

namespace Domain.Services;

public interface IReservationService
{
    IReadOnlyList<Reservation> GetAll(
        int? propertyId,
        string guestName,
        DateTime? dateFrom,
        DateTime? dateTo );

    Reservation GetById( int id );

    void Create( Reservation reservation );

    void Cancel( int id );
}
