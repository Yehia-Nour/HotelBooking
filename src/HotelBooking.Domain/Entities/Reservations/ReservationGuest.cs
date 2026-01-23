using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Guests;

namespace HotelBooking.Domain.Entities.Reservations
{
    public class ReservationGuest : BaseEntity
    {
        public int ReservationRoomID { get; set; }
        public ReservationRoom ReservationRoom { get; set; } = default!;

        public int GuestID { get; set; }
        public Guest Guest { get; set; } = default!;
    }
}