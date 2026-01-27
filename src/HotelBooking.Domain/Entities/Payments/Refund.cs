using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Payments
{
    public class Refund : BaseEntity
    {
        public decimal RefundAmount { get; set; }
        public string RefundReason { get; set; } = default!;
        public RefundStatus RefundStatus { get; set; } = default!;
        public DateTime RefundDate { get; set; }
        public decimal CancellationCharge { get; set; }
        public decimal? NetRefundAmount { get; set; }

        public string ProcessedByUserID { get; set; } = default!;

        public int RefundMethodID { get; set; }
        public RefundMethod RefundMethod { get; set; } = default!;

        public int PaymentID { get; set; }
        public Payment Payment { get; set; } = default!;

        public int? CancellationRequestId { get; set; }
        public CancellationRequest? CancellationRequest { get; set; }
    }
}
