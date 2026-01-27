using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Requests
{
    public class UpdateCancellationPolicyCommand : IRequest<Result>
    {
        public int Id { get; init; }
        public string? Description { get; init; }
        public decimal? CancellationChargePercentage { get; init; }
        public decimal? MinimumCharge { get; init; }
        public DateTime? EffectiveFromDate { get; init; }
        public DateTime? EffectiveToDate { get; init; }
    }
}
