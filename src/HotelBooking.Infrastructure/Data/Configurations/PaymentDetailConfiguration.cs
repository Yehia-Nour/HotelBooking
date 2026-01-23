using HotelBooking.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class PaymentDetailConfiguration : IEntityTypeConfiguration<PaymentDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentDetail> builder)
        {
            builder.Property(pd => pd.Amount)
                   .HasColumnType("decimal(10,2)");

            builder.Property(pd => pd.GST)
                   .HasColumnType("decimal(10,2)");

            builder.Property(pd => pd.TotalAmount)
                   .HasColumnType("decimal(10,2)");
        }
    }
}
