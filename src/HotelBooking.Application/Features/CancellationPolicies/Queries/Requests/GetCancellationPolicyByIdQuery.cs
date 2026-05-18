namespace HotelBooking.Application.Features.CancellationPolicies.Queries.Requests
{
    public record GetCancellationPolicyByIdQuery(int Id) : IRequest<Result<CancellationPolicyDTO>>;
}
