using FluentValidation;
using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Validators
{
    internal class CreateRoomAmenityValidator : AbstractValidator<CreateRoomAmenityCommand>
    {
        public CreateRoomAmenityValidator()
        {
            RuleFor(cr => cr.AmenityId).RequiredNumberField(nameof(CreateRoomAmenityCommand.AmenityId));

            RuleFor(cr => cr.RoomTypeId).RequiredNumberField(nameof(CreateRoomAmenityCommand.RoomTypeId));
        }
    }
}
