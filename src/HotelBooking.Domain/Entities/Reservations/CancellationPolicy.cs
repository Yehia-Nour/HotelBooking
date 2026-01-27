using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class CancellationPolicy : BaseEntity
    {
        public string? Description { get; set; }

        public decimal? CancellationChargePercentage { get; set; }
        public decimal? MinimumCharge { get; set; }

        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
    }
}
