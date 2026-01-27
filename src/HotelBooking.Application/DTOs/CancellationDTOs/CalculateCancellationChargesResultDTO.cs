namespace HotelBooking.Application.DTOs.CancellationDTOs
{
    public record CalculateCancellationChargesResultDTO
    {
        public decimal TotalCost { get; init; }
        public decimal CancellationCharge { get; init; }
        public decimal CancellationPercentage { get; init; }
        public string PolicyDescription { get; init; } = default!;
        public bool IsFullCancellation { get; init; }
    }
}
