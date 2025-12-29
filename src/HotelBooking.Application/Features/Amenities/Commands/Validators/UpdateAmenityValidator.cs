using FluentValidation;
using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.Amenities.Commands.Validators
{
    public class UpdateAmenityValidator : AbstractValidator<UpdateAmenityCommand>
    {
        public UpdateAmenityValidator()
        {
            RuleFor(ua => ua.AmenityId).RequiredNumberField(nameof(UpdateAmenityCommand.AmenityId));

            RuleFor(ca => ca.Name)
                .RequiredField(nameof(UpdateAmenityCommand.Name))
                .MaxLengthField(nameof(UpdateAmenityCommand.Name), 100);

            RuleFor(ca => ca.Description)
                .RequiredField(nameof(UpdateAmenityCommand.Description))
                .MaxLengthField(nameof(UpdateAmenityCommand.Description), 255);
        }
    }
}
