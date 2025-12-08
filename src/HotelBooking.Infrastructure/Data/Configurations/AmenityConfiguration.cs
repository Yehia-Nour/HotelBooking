using HotelBooking.Domain.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100);

            builder.Property(a => a.Description).HasMaxLength(255);

            builder.Property(a => a.CreatedBy).HasMaxLength(100);
            builder.Property(a => a.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(a => a.ModifiedBy).HasMaxLength(100);
        }
    }
}
