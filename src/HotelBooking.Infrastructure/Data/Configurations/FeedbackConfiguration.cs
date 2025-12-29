using HotelBooking.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Data.Configurations
{
    internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(f => f.Comment).HasMaxLength(1000);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CHK_Rating_Between_1_5",
                    "Rating BETWEEN 1 AND 5"
                );
            });
        }
    }
}
