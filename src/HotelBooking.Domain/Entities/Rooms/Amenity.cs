using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Domain.Entities.Rooms
{
    public class Amenity : AuditableEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public ICollection<RoomAmenity> RoomAmenities { get; set; } = default!;
    }
}