using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Payments
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }

        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;

        public int PaymentBatchID { get; set; }
        public PaymentBatch PaymentBatch { get; set; } = default!;
    }
}