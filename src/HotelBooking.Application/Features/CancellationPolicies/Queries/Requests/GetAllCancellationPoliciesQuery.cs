using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Queries.Requests
{
    public record GetAllCancellationPoliciesQuery() : IRequest<Result<IEnumerable<CancellationPolicyDTO>>>;
}
