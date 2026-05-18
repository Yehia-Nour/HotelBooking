namespace HotelBooking.Application.Features.Refunds.Commands.Requests
{
    public record ProcessRefundWithUserCommand(string ProcessedByUserId, ProcessRefundCommand Command) : IRequest<Result<int>>;
}
