using HotelBooking.Domain.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class CountryConfigruation : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.CountryName).HasMaxLength(50);

            builder.Property(c => c.CountryCode).HasMaxLength(10);

            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.ModifiedBy).HasMaxLength(100);
        }
    }
}