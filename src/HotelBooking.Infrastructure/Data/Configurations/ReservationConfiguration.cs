using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasMany(r => r.ReservationRooms)
                   .WithOne(rr => rr.Reservation)
                   .HasForeignKey(rr => rr.ReservationID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Payments)
                   .WithOne(p => p.Reservation)
                   .HasForeignKey(p => p.ReservationID);
        }
    }
}
