namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public record UpdatePaymentStatusWithUserCommand(string UserId, UpdatePaymentStatusCommandRequest Command) : IRequest<Result>;
}
