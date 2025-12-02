using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = default!;
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public BookingStatus Status { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }

        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; } = default!;

        public ICollection<Reservation> Reservations { get; set; } = default!;
    }
}