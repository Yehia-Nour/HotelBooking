namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Validators
{
    public class CreateCancellationPolicyCommandValidator : AbstractValidator<CreateCancellationPolicyCommand>
    {
        public CreateCancellationPolicyCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.CancellationChargePercentage)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.MinimumCharge)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.EffectiveFromDate)
                .NotEmpty();

            RuleFor(x => x.EffectiveToDate)
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.EffectiveToDate.Date >= x.EffectiveFromDate.Date)
                .WithMessage("EffectiveToDate must be >= EffectiveFromDate.");
        }
    }
}
