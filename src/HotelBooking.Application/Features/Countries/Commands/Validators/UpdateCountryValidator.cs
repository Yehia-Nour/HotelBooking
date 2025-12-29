using FluentValidation;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.Countries.Commands.Validators
{
    internal class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryValidator()
        {
            RuleFor(c => c.CountryId).RequiredNumberField(nameof(UpdateCountryCommand.CountryId));

            RuleFor(c => c.CountryName)
                              .RequiredField(nameof(UpdateCountryCommand.CountryName))
                              .MaxLengthField(nameof(UpdateCountryCommand.CountryName), 50);

            RuleFor(c => c.CountryCode)
                .RequiredField(nameof(UpdateCountryCommand.CountryCode))
                .MaxLengthField(nameof(UpdateCountryCommand.CountryCode), 10);
        }
    }

}
