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
    internal class PaymentBatcheConfiguration : IEntityTypeConfiguration<PaymentBatch>
    {
        public void Configure(EntityTypeBuilder<PaymentBatch> builder)
        {
            builder.Property(pb => pb.PaymentMethod).HasMaxLength(50);

            builder.Property(pb => pb.TotalAmount).HasPrecision(10, 2);
        }
    }
}
