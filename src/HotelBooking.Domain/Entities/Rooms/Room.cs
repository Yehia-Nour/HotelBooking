using HotelBooking.Domain.Entities.Common;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class Room : AuditableEntity
    {
        public string RoomNumber { get; set; } = default!;
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public BookingStatus Status { get; set; }
        public bool IsActive { get; set; } = true;

        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; } = default!;

        public ICollection<Reservation> Reservations { get; set; } = default!;
    }
}