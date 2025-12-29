using HotelBooking.Domain.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasIndex(r => r.RoomNumber).IsUnique();
            builder.Property(r => r.RoomNumber).HasMaxLength(50);

            builder.Property(r => r.Price).HasPrecision(10, 2);


            builder.Property(r => r.BedType).HasMaxLength(50);

            builder.Property(r => r.ViewType).HasMaxLength(50);

            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.ModifiedBy).HasMaxLength(100);
        }
    }
}
