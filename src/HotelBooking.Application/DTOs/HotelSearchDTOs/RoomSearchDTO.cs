namespace HotelBooking.Application.DTOs.HotelSearchDTOs
{
    public class RoomSearchDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = default!;
        public decimal Price { get; set; }
        public string BedType { get; set; } = default!;
        public string ViewType { get; set; } = default!;
        public string Status { get; set; } = default!;
        public RoomTypeSearchDTO RoomType { get; set; } = default!;
    }
}