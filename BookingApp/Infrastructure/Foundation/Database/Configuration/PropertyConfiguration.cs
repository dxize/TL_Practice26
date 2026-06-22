using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Database.Configuration;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) );
        builder.HasKey( property => property.Id );

        builder.Property( property => property.Name )
               .HasMaxLength( 300 )
               .IsRequired();

        builder.Property( property => property.Country )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( property => property.City )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( property => property.Address )
               .HasMaxLength( 500 )
               .IsRequired();

        builder.Property( property => property.Latitude )
               .IsRequired();

        builder.Property( property => property.Longitude )
               .IsRequired();

        builder.HasMany( property => property.RoomTypes )
               .WithOne( roomType => roomType.Property )
               .HasForeignKey( roomType => roomType.PropertyId );

        builder.HasMany( property => property.Reservations )
               .WithOne( reservation => reservation.Property )
               .HasForeignKey( reservation => reservation.PropertyId )
               .OnDelete( DeleteBehavior.Restrict );
    }
}
