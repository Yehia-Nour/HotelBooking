using FluentValidation;
using HotelBooking.Application.Features.HotelBooking.Commands.Requests;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Validators
{
    public class AddGuestsToReservationRequestValidator : AbstractValidator<AddGuestsToReservationCommand>
    {
        public AddGuestsToReservationRequestValidator()
        {
            RuleFor(x => x.ReservationId)
                .GreaterThan(0);

            RuleFor(x => x.Guests)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("At least one guest is required.");

            RuleForEach(x => x.Guests).ChildRules(g =>
            {
                g.RuleFor(x => x.RoomId).GreaterThan(0);

                g.RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
                g.RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

                g.RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
                g.RuleFor(x => x.Phone).NotEmpty().MaximumLength(15);

                g.RuleFor(x => x.Address).NotEmpty().MaximumLength(500);

                g.RuleFor(x => x.CountryId).GreaterThan(0);
                g.RuleFor(x => x.StateId).GreaterThan(0);

                g.RuleFor(x => x.AgeGroup).IsInEnum();
            });
        }
    }
}
