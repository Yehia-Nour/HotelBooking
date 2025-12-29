using FluentValidation;
using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.Amenities.Commands.Validators
{
    public class CreateAmenityValidator : AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityValidator()
        {
            RuleFor(ca => ca.Name)
                .RequiredField(nameof(CreateAmenityCommand.Name))
                .MaxLengthField(nameof(CreateAmenityCommand.Name), 100);

            RuleFor(ca => ca.Description)
                .RequiredField(nameof(CreateAmenityCommand.Description))
                .MaxLengthField(nameof(CreateAmenityCommand.Description), 255);
        }
    }
}