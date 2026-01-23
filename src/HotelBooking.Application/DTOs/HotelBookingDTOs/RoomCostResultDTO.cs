namespace HotelBooking.Application.DTOs.HotelBookingDTOs
{
    public class RoomCostResultDTO
    {
        public decimal Amount { get; init; }
        public decimal GST { get; init; }
        public decimal TotalAmount { get; init; }
        public int NumberOfNights { get; init; }
        public List<RoomCostBreakdownDTO> Rooms { get; init; } = [];
    }
}