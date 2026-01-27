namespace HotelBooking.Application.DTOs.CancellationDTOs
{
    public record CancellationPolicyDTO
    {
        public int Id { get; init; }
        public string Description { get; init; } = default!;
        public decimal CancellationChargePercentage { get; init; }
        public decimal? MinimumCharge { get; init; }
        public DateTime EffectiveFromDate { get; init; }
        public DateTime EffectiveToDate { get; init; }
    }
}
