namespace HotelBooking.Domain.Entities.Reservations
{
    public class ReservationRoom : BaseEntity
    {
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; } = default!;

        public int RoomID { get; set; }
        public Room Room { get; set; } = default!;

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public ICollection<ReservationGuest> ReservationGuests { get; set; } = [];
        public ICollection<PaymentDetail> PaymentDetails { get; set; } = [];
    }
}
