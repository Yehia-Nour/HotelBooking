using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.HasOne(rr => rr.Room)
                   .WithMany(r => r.ReservationRooms)
                   .HasForeignKey(rr => rr.RoomID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rr => rr.ReservationGuests)
                   .WithOne(rg => rg.ReservationRoom)
                   .HasForeignKey(rg => rg.ReservationRoomID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rr => rr.PaymentDetails)
                   .WithOne(pd => pd.ReservationRoom)
                   .HasForeignKey(pd => pd.ReservationRoomID);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CHK_ResRoomDates",
                    "[CheckOutDate] > [CheckInDate]"
                );
            });
        }
    }
}
