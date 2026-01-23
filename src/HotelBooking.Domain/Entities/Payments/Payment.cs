using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Payments
{
    public class Payment : BaseEntity
    {
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;

        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string? FailureReason { get; set; }

        public ICollection<PaymentDetail> PaymentDetails { get; set; } = [];
    }
}