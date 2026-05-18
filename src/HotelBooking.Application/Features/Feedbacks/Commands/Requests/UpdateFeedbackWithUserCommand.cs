namespace HotelBooking.Application.Features.Feedbacks.Commands.Requests
{
    public record UpdateFeedbackWithUserCommand(string UserId, UpdateFeedbackCommand Command) : IRequest<Result>;
}
