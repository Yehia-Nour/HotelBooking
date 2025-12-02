using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Rooms;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class Reservation
    {
        public int ReservationID { get; set; } = default!;
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; } = default!;

        public string UserID { get; set; } = default!;

        public int RoomID { get; set; }
        public Room Room { get; set; } = default!;

        public ICollection<ReservationGuest> ReservationGuests { get; set; } = default!;
        public ICollection<Payment> Payments { get; set; } = default!;
        public ICollection<Cancellation> Cancellations { get; set; } = default!;
    }
}