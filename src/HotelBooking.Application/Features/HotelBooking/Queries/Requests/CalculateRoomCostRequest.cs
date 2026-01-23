namespace HotelBooking.Application.Features.HotelBooking.Queries.Requests
{
    public class CalculateRoomCostRequest
    {
        public List<int> RoomIds { get; init; } = [];
        public DateTime? CheckInDate { get; init; }
        public DateTime? CheckOutDate { get; init; }
    }
}
