using HotelBooking.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class RefundMethodConfiguration : IEntityTypeConfiguration<RefundMethod>
    {
        public void Configure(EntityTypeBuilder<RefundMethod> builder)
        {
            builder.Property(rm => rm.MethodName).HasMaxLength(50);
        }
    }
}
