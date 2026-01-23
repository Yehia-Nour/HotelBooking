namespace HotelBooking.Application.DTOs.HotelBookingDTOs
{
    public class RoomCostBreakdownDTO
    {
        public int RoomId { get; init; }
        public string RoomNumber { get; init; } = default!;
        public decimal PricePerNight { get; init; }
        public decimal TotalPrice { get; init; }
    }
}