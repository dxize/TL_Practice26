using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly BookingDbContext _dbContext;

    public EfReservationRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Reservation> GetAll()
    {
        return _dbContext.Set<Reservation>().ToList();
    }

    public Reservation GetById( int id )
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
        Reservation existingReservation = GetById( id );
        _dbContext.Set<Reservation>().Remove( existingReservation );
        _dbContext.SaveChanges();
    }
}