using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class CancellationPolicyConfiguration : IEntityTypeConfiguration<CancellationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancellationPolicy> builder)
        {
            builder.Property(x => x.Description)
                   .HasMaxLength(255);

            builder.Property(x => x.CancellationChargePercentage)
                   .HasPrecision(5, 2);

            builder.Property(x => x.MinimumCharge)
                   .HasPrecision(10, 2);
        }
    }
}
