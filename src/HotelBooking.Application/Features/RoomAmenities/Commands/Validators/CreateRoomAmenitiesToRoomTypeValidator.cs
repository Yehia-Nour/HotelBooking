using FluentValidation;
using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Validators
{
    internal class CreateRoomAmenitiesToRoomTypeValidator : AbstractValidator<CreateRoomAmenitiesToRoomTypeCommand>
    {
        private const int _maxAmenityIdsCount = 10;

        public CreateRoomAmenitiesToRoomTypeValidator()
        {
            RuleFor(cr => cr.AmenityIds)
                .NotEmpty()
                .Must(cr => cr.Count <= _maxAmenityIdsCount)
                .WithMessage($"Maximum allowed Amenity Ids is {_maxAmenityIdsCount}");

            RuleForEach(cr => cr.AmenityIds).GreaterThan(0);
        }
    }
}
