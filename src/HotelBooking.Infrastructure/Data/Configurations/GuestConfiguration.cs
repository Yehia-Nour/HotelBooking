using HotelBooking.Domain.Entities.Guests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(g => g.FirstName).HasMaxLength(50);

            builder.Property(g => g.LastName).HasMaxLength(50);

            builder.Property(g => g.Email).HasMaxLength(50);

            builder.Property(g => g.Phone).HasMaxLength(11);

            builder.Property(g => g.Address).HasMaxLength(255);

            builder.Property(g => g.CreatedBy).HasMaxLength(100);
            builder.Property(g => g.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.Property(g => g.ModifiedBy).HasMaxLength(100);

            builder.HasOne(g => g.State)
                   .WithMany()
                   .HasForeignKey(g => g.StateID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
