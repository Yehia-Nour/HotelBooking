namespace HotelBooking.Application.Features.CancellationPolicies.Queries.Requests
{
    public record GetAllCancellationPoliciesQuery() : IRequest<Result<IEnumerable<CancellationPolicyDTO>>>;
}
