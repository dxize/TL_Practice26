using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFReservationRepository : IReservationRepository
{
    private readonly BookingDbContext _dbContext;

    public EFReservationRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Reservation> GetAllReservations()
    {
        return _dbContext.Set<Reservation>().ToList();
    }

    public Reservation GetReservationById( int id )
    {
        return _dbContext.Set<Reservation>().FirstOrDefault( reservation => reservation.Id == id );
    }

    public IReadOnlyList<Reservation> GetActiveReservationsByRoomTypeAndDates(
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate )
    {
        return _dbContext.Set<Reservation>()
            .Where( reservation =>
                reservation.RoomTypeId == roomTypeId &&
                !reservation.IsCanceled &&
                reservation.ArrivalDate < departureDate &&
                arrivalDate < reservation.DepartureDate )
            .ToList();
    }

    public void Save( Reservation reservation )
    {
        _dbContext.Set<Reservation>().Add( reservation );
        _dbContext.SaveChanges();
    }

    public void Update( Reservation reservation )
    {
        _dbContext.Set<Reservation>().Update( reservation );
        _dbContext.SaveChanges();
    }

    public void Delete( int id )
    {
        Reservation existingReservation = GetReservationById( id );
        _dbContext.Set<Reservation>().Remove( existingReservation );
        _dbContext.SaveChanges();
    }
}