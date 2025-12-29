namespace HotelBooking.Application.DTOs.RoomDTOs
{
    public record RoomDTO
    {
        public int Id { get; init; }
        public string RoomNumber { get; init; } = default!;
        public int RoomTypeID { get; init; }
        public decimal Price { get; init; }
        public string BedType { get; init; } = default!;
        public string ViewType { get; init; } = default!;
        public string Status { get; init; } = default!;
        public bool IsActive { get; init; }
    }
}
