namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public class CreateReservationCommand
    {
        public List<int> RoomIds { get; init; } = [];
        public DateTime? CheckInDate { get; init; }
        public DateTime? CheckOutDate { get; init; }
    }
}
