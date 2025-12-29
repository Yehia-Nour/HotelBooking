using HotelBooking.Domain.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.Property(s => s.StateName).HasMaxLength(50);

            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.ModifiedBy).HasMaxLength(100);
        }
    }
}
