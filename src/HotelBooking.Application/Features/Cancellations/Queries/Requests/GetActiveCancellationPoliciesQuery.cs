namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public record GetActiveCancellationPoliciesQuery() : IRequest<Result<IEnumerable<CancellationPolicyDTO>>>;
}
