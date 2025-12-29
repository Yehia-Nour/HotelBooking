using FluentValidation;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.Countries.Commands.Validators
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryValidator()
        {
            RuleFor(c => c.CountryName)
                    .RequiredField(nameof(CreateCountryCommand.CountryName))
                    .MaxLengthField(nameof(CreateCountryCommand.CountryName), 50);

            RuleFor(c => c.CountryCode)
                .RequiredField(nameof(CreateCountryCommand.CountryCode))
                .MaxLengthField(nameof(CreateCountryCommand.CountryCode), 10);
        }
    }
}
