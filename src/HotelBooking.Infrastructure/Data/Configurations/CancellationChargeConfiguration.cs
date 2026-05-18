namespace HotelBooking.Infrastructure.Data.Configurations
{
    public class CancellationChargeConfiguration : IEntityTypeConfiguration<CancellationCharge>
    {
        public void Configure(EntityTypeBuilder<CancellationCharge> builder)
        {

            builder.HasKey(x => x.CancellationRequestId);

            builder.Property(x => x.CancellationRequestId)
                   .HasColumnName("CancellationRequestID");

            builder.Property(x => x.TotalCost)
                   .HasPrecision(10, 2);

            builder.Property(x => x.CancellationChargeAmount)
                   .HasPrecision(10, 2);

            builder.Property(x => x.CancellationPercentage)
                   .HasPrecision(10, 2);

            builder.Property(x => x.MinimumCharge)
                   .HasPrecision(10, 2);

            builder.Property(x => x.PolicyDescription)
                   .HasMaxLength(255);

            builder.HasOne(x => x.CancellationRequest)
                   .WithOne(x => x.CancellationCharge)
                   .HasForeignKey<CancellationCharge>(x => x.CancellationRequestId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
