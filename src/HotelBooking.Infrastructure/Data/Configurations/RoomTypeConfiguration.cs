using HotelBooking.Domain.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.Property(rt => rt.TypeName).HasMaxLength(50);

            builder.Property(rt => rt.AccessibilityFeatures).HasMaxLength(255);

            builder.Property(rt => rt.Description).HasMaxLength(255);

            builder.Property(rt => rt.CreatedBy).HasMaxLength(100);
            builder.Property(rt => rt.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(rt => rt.ModifiedBy).HasMaxLength(100);

            builder.HasIndex(rt => rt.TypeName);
        }
    }
}
