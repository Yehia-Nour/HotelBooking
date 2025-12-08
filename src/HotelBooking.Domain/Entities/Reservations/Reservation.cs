using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Rooms;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class Reservation : AuditableEntity
    {
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
    }
}