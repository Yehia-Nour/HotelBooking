using FluentValidation;
using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.States.Commands.Validators
{
    public class CreateStateValidator : AbstractValidator<CreateStateCommand>
    {
        public CreateStateValidator()
        {
            RuleFor(s => s.StateName)
                .RequiredField(nameof(CreateStateCommand.StateName))
                .MaxLengthField(nameof(CreateStateCommand.StateName), 100);

            RuleFor(s => s.CountryID)
                .RequiredNumberField(nameof(CreateStateCommand.CountryID));
        }
    }
}
