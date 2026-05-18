namespace HotelBooking.Domain.Entities.Reservations
{
    public class CancellationCharge : BaseEntity
    {
        public decimal? TotalCost { get; set; }
        public decimal? CancellationChargeAmount { get; set; }
        public decimal? CancellationPercentage { get; set; }
        public decimal? MinimumCharge { get; set; }
        public string? PolicyDescription { get; set; }

        public int CancellationRequestId { get; set; }
        public CancellationRequest? CancellationRequest { get; set; }
    }
}