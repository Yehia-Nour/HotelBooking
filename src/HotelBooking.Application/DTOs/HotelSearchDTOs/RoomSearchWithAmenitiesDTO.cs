namespace HotelBooking.Application.DTOs.HotelSearchDTOs
{
    public class RoomSearchWithAmenitiesDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = default!;
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public string Status { get; set; } = default!;
        public RoomTypeSearchWithAmenitiesDTO RoomType { get; set; } = default!;
    }
}
