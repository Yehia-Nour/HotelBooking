using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.ModifiedBy).HasMaxLength(100);

            builder.ToTable(t => t
                .HasCheckConstraint("CHK_CheckOutDate", "CheckOutDate > CheckInDate")
            );
        }
    }
}
