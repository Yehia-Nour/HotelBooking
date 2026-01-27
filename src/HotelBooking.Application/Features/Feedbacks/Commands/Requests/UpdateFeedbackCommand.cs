namespace HotelBooking.Application.Features.Feedbacks.Commands.Requests
{
    public class UpdateFeedbackCommand
    {
        public int FeedbackId { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
    }
}
