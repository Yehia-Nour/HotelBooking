using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class RoomAmenity : BaseEntity
    {
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; } = default!;

        public int AmenityID { get; set; }
        public Amenity Amenity { get; set; } = default!;
    }
}