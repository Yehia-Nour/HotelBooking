namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class RoomsAvailabilityFilterValidator : AbstractValidator<RoomsAvailabilityFilter>
    {
        public RoomsAvailabilityFilterValidator()
        {
            RuleFor(x => x.CheckInDate)
                .NotEmpty()
                .WithMessage("Check-in date is required.")
                .Must(BeValidDate)
                .WithMessage("Check-in date must be a valid date.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty()
                .WithMessage("Check-out date is required.")
                .Must(BeValidDate)
                .WithMessage("Check-out date must be a valid date.");

            RuleFor(x => x)
                .Must(x => x.CheckOutDate > x.CheckInDate)
                .WithMessage("Check-out date must be after check-in date.");
        }

        private bool BeValidDate(DateTime date)
        {
            return date != default;
        }
    }
}
