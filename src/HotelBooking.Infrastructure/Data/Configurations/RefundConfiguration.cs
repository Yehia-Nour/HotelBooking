using HotelBooking.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.Property(r => r.RefundReason).HasMaxLength(255);

            builder.Property(r => r.RefundAmount).HasPrecision(10, 2);

            builder.Property(r => r.RefundDate).HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.RefundStatus).HasMaxLength(50);

            builder.Property(x => x.NetRefundAmount).HasPrecision(10, 2);

            builder.HasOne(x => x.Payment)
                   .WithMany()
                   .HasForeignKey(x => x.PaymentID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RefundMethod)
                   .WithMany()
                   .HasForeignKey(x => x.RefundMethodID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CancellationRequest)
                   .WithMany(x => x.Refunds)
                   .HasForeignKey(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
