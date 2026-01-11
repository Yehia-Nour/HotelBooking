namespace HotelBooking.Application.DTOs.HotelSearchDTOs
{
    public class RoomTypeSearchWithAmenitiesDTO : RoomTypeSearchDTO
    {
        public List<AmenitySearchDTO> Amenities { get; set; } = [];
    }
}
