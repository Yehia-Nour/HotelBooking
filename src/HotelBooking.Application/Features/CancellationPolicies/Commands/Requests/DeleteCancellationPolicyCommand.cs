namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Requests
{
    public record DeleteCancellationPolicyCommand(int Id) : IRequest<Result>;
}
