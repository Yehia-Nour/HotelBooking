using FluentValidation;
using HotelBooking.Application.Features.CancellationPolicies.Commands.Requests;

namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Validators
{
    public class UpdateCancellationPolicyCommandValidator : AbstractValidator<UpdateCancellationPolicyCommand>
    {
        public UpdateCancellationPolicyCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Description).MaximumLength(255);

            RuleFor(x => x.CancellationChargePercentage)
                .InclusiveBetween(0, 100)
                .When(x => x.CancellationChargePercentage.HasValue);

            RuleFor(x => x.MinimumCharge)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinimumCharge.HasValue);

            RuleFor(x => x)
                .Must(x => !x.EffectiveFromDate.HasValue || !x.EffectiveToDate.HasValue
                           || x.EffectiveToDate.Value.Date >= x.EffectiveFromDate.Value.Date)
                .WithMessage("EffectiveToDate must be >= EffectiveFromDate.");
        }
    }
}
