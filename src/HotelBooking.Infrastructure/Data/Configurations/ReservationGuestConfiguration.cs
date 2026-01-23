using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class ReservationGuestConfiguration : IEntityTypeConfiguration<ReservationGuest>
    {
        public void Configure(EntityTypeBuilder<ReservationGuest> builder)
        {
            builder.HasOne(rg => rg.Guest)
                   .WithMany(g => g.ReservationGuests)
                   .HasForeignKey(rg => rg.GuestID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
