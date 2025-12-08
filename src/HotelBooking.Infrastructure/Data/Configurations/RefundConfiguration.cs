using HotelBooking.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
