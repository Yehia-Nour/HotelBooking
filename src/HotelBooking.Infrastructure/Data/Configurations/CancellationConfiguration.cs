using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class CancellationConfiguration : IEntityTypeConfiguration<CancellationRequest>
    {
        public void Configure(EntityTypeBuilder<CancellationRequest> builder)
        {
            builder.Property(x => x.CancellationType)
                    .HasMaxLength(50);

            builder.Property(x => x.CancellationReason)
                   .HasMaxLength(255);

            builder.Property(x => x.RequestedOn)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.ModifiedBy).HasMaxLength(100);

            builder.HasOne(c => c.Reservation)
                .WithOne()
                .HasForeignKey<CancellationRequest>(c => c.ReservationID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(x => x.CancellationDetails)
                   .WithOne(x => x.CancellationRequest)
                   .HasForeignKey(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CancellationCharge)
                   .WithOne(x => x.CancellationRequest)
                   .HasForeignKey<CancellationCharge>(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Refunds)
                   .WithOne(x => x.CancellationRequest)
                   .HasForeignKey(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
