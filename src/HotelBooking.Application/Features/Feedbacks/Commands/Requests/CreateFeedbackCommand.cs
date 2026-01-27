namespace HotelBooking.Application.Features.Feedbacks.Commands.Requests
{
    public class CreateFeedbackCommand
    {
        public int ReservationId { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
    }
}
