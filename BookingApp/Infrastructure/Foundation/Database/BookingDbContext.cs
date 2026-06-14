using Infrastructure.Foundation.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Database;

public class BookingDbContext : DbContext
{
    public BookingDbContext( DbContextOptions options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
    }
}