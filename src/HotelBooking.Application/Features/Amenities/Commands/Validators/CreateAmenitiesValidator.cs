using FluentValidation;
using HotelBooking.Application.Features.Amenities.Commands.Requests;

namespace HotelBooking.Application.Features.Amenities.Commands.Validators
{
    public class CreateAmenitiesValidator : AbstractValidator<CreateAmenitiesCommand>
    {
        private const int MaxAmenitiesCount = 10;

        public CreateAmenitiesValidator()
        {
            RuleFor(ca => ca.Amenities)
                .NotEmpty()
                .Must(ca => ca.Count <= MaxAmenitiesCount)
                .WithMessage($"Maximum allowed amenities is {MaxAmenitiesCount}");

            RuleForEach(ca => ca.Amenities)
                .SetValidator(new CreateAmenityValidator());
        }
    }
}
