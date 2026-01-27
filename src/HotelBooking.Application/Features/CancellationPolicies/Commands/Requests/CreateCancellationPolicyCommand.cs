using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Requests
{
    public class CreateCancellationPolicyCommand : IRequest<Result<int>>
    {
        public string Description { get; init; } = default!;
        public decimal CancellationChargePercentage { get; init; }
        public decimal MinimumCharge { get; init; }
        public DateTime EffectiveFromDate { get; init; }
        public DateTime EffectiveToDate { get; init; }
    }
}
