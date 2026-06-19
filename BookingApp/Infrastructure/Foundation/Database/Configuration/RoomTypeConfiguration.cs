using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.Configuration;

internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) );
        builder.HasKey( roomType => roomType.Id );

        builder.Property( roomType => roomType.Name )
               .HasMaxLength( 300 )
               .IsRequired();

        builder.Property( roomType => roomType.DailyPrice )
               .HasPrecision( 18, 2 )
               .IsRequired();

        builder.Property( roomType => roomType.Currency )
               .HasMaxLength( 10 )
               .IsRequired();

        builder.Property( roomType => roomType.MinPersonCount )
               .IsRequired();

        builder.Property( roomType => roomType.MaxPersonCount )
               .IsRequired();

        builder.Property( roomType => roomType.TotalRooms )
               .IsRequired();

        builder.Property( roomType => roomType.Services )
               .HasMaxLength( 1000 )
               .IsRequired();

        builder.Property( roomType => roomType.Amenities )
               .HasMaxLength( 1000 )
               .IsRequired();

        builder.HasOne( roomType => roomType.Property )
               .WithMany( property => property.RoomTypes )
               .HasForeignKey( roomType => roomType.PropertyId );

        builder.HasMany( roomType => roomType.Reservations )
               .WithOne( reservation => reservation.RoomType )
               .HasForeignKey( reservation => reservation.RoomTypeId )
               .OnDelete( DeleteBehavior.Restrict );
    }
}
