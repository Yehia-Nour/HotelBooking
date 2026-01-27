namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public class CalculateCancellationChargesRequest
    {
        public int ReservationId { get; init; }
        public List<int> RoomsCancelled { get; init; } = [];
    }
}
