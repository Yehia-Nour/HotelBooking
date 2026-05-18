namespace HotelBooking.Application.Features.Cancellations.Commands.Requests
{
    public record CreateCancellationRequestWithUserCommand(string UserId, CreateCancellationRequestCommand Command) : IRequest<Result<int>>;
}
