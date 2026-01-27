using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class CancellationDetailConfiguration : IEntityTypeConfiguration<CancellationDetail>
    {
        public void Configure(EntityTypeBuilder<CancellationDetail> builder)
        {
            builder.HasOne(x => x.CancellationRequest)
                   .WithMany(x => x.CancellationDetails)
                   .HasForeignKey(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ReservationRoom)
                   .WithMany()
                   .HasForeignKey(x => x.ReservationRoomId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
