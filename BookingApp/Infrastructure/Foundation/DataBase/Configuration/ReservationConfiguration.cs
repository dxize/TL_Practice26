using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configuration;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) );
        builder.HasKey( reservation => reservation.Id );

        builder.Property( reservation => reservation.ArrivalDate )
               .HasColumnType( "date" )
               .IsRequired();

        builder.Property( reservation => reservation.DepartureDate )
               .HasColumnType( "date" )
               .IsRequired();

        builder.Property( reservation => reservation.ArrivalTime )
               .HasColumnType( "time" )
               .IsRequired();

        builder.Property( reservation => reservation.DepartureTime )
               .HasColumnType( "time" )
               .IsRequired();

        builder.Property( reservation => reservation.GuestName )
               .HasMaxLength( 300 )
               .IsRequired();

        builder.Property( reservation => reservation.GuestPhoneNumber )
               .HasMaxLength( 50 )
               .IsRequired();

        builder.Property( reservation => reservation.Guests )
               .IsRequired();

        builder.Property( reservation => reservation.Total )
               .HasPrecision( 18, 2 )
               .IsRequired();

        builder.Property( reservation => reservation.Currency )
               .HasMaxLength( 10 )
               .IsRequired();

        builder.Property( reservation => reservation.IsCanceled )
               .IsRequired();

        builder.HasOne( reservation => reservation.Property )
               .WithMany( property => property.Reservations )
               .HasForeignKey( reservation => reservation.PropertyId )
               .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( reservation => reservation.RoomType )
               .WithMany( roomType => roomType.Reservations )
               .HasForeignKey( reservation => reservation.RoomTypeId )
               .OnDelete( DeleteBehavior.Restrict );
    }
}