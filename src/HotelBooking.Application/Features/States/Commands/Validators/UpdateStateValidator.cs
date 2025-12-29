using FluentValidation;
using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.States.Commands.Validators
{
    internal class UpdateStateValidator : AbstractValidator<UpdateStateCommand>
    {
        public UpdateStateValidator()
        {
            RuleFor(s => s.StateId).RequiredNumberField(nameof(UpdateStateCommand.StateId));

            RuleFor(s => s.StateName)
                .RequiredField(nameof(UpdateStateCommand.StateName))
                .MaxLengthField(nameof(UpdateStateCommand.StateName), 100);

            RuleFor(s => s.CountryID)
                .RequiredNumberField(nameof(UpdateStateCommand.CountryID));
        }
    }
}
