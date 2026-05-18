namespace HotelBooking.Application.Features.Feedbacks.Commands.Requests
{
    public record DeleteFeedbackWithUserCommand(string UserId, int FeedbackId) : IRequest<Result>;
}
