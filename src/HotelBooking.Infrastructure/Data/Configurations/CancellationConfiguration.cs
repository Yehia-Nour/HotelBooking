using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class CancellationConfiguration : IEntityTypeConfiguration<Cancellation>
    {
        public void Configure(EntityTypeBuilder<Cancellation> builder)
        {
            builder.Property(c => c.Reason).HasMaxLength(255);

            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.ModifiedBy).HasMaxLength(100);

            builder.Property(c => c.CancellationFee).HasPrecision(10, 2);

            builder.HasOne(c => c.Reservation)
                .WithOne()
                .HasForeignKey<Cancellation>(c => c.ReservationID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
