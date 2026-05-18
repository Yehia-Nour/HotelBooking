namespace HotelBooking.Application.Features.Cancellations.Commands.Requests
{
    public record ReviewCancellationRequestWithAdminCommand(string AdminUserId, ReviewCancellationRequestCommand Command) : IRequest<Result>;
}
