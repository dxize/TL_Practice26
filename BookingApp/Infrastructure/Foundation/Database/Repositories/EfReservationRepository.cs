using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Database.Repositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly BookingDbContext _dbContext;

    public EfReservationRepository( BookingDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Reservation> GetAll(
        int? propertyId = null,
        string guestName = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null )
    {
        IQueryable<Reservation> query = _dbContext.Set<Reservation>().AsQueryable();

        if ( propertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == propertyId.Value );
        }

        if ( !string.IsNullOrWhiteSpace( guestName ) )
        {
            query = query.Where( r => r.GuestName.ToLower().Contains( guestName.ToLower() ) );
        }

        if ( dateFrom.HasValue )
        {
            query = query.Where( r => r.DepartureDate > dateFrom.Value );
        }

        if ( dateTo.HasValue )
        {
            query = query.Where( r => r.ArrivalDate < dateTo.Value );
        }

        return query.ToList();
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

    public void Delete( Reservation reservation )
    {
        _dbContext.Set<Reservation>().Remove( reservation );
        _dbContext.SaveChanges();
    }
}
