using HotelBooking.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.GST)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.TotalAmount)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.PaymentStatus)
                   .HasDefaultValue(PaymentStatus.Pending);

            builder.HasMany(p => p.PaymentDetails)
                   .WithOne(pd => pd.Payment)
                   .HasForeignKey(pd => pd.PaymentID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
