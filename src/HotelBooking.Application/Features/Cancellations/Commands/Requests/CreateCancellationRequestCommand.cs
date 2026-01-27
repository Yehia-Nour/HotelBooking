namespace HotelBooking.Application.Features.Cancellations.Commands.Requests
{
    public class CreateCancellationRequestCommand
    {
        public int ReservationId { get; init; }
        public List<int> RoomsCancelled { get; init; } = [];
        public string CancellationReason { get; init; } = default!;
    }
}
