using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Payments
{
    public class PaymentDetail : BaseEntity
    {
        public int PaymentID { get; set; }
        public Payment Payment { get; set; } = default!;

        public int ReservationRoomID { get; set; }
        public ReservationRoom ReservationRoom { get; set; } = default!;

        public decimal Amount { get; set; }
        public int NumberOfNights { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
