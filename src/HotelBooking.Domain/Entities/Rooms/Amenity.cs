namespace HotelBooking.Domain.Entities.Rooms
{
    public class Amenity
    {
        public int AmenityID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = default!;
        public DateTime ModifiedDate { get; set; }

        public ICollection<RoomAmenity> RoomAmenities { get; set; } = default!;
    }
}