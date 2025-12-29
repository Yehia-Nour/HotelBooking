using FluentValidation;
using HotelBooking.Application.Features.RoomTypes.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Validators
{
    internal class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeValidator()
        {
            RuleFor(ur => ur.RoomTypeId).RequiredNumberField(nameof(UpdateRoomTypeCommand.RoomTypeId));

            RuleFor(ur => ur.TypeName)
                .RequiredField(nameof(UpdateRoomTypeCommand.TypeName))
                .MaxLengthField(nameof(UpdateRoomTypeCommand.TypeName), 50);


            RuleFor(ur => ur.AccessibilityFeatures)
                .RequiredField(nameof(UpdateRoomTypeCommand.AccessibilityFeatures))
                .MaxLengthField(nameof(UpdateRoomTypeCommand.AccessibilityFeatures), 255);

            RuleFor(ur => ur.Description)
                .RequiredField(nameof(UpdateRoomTypeCommand.Description))
                .MaxLengthField(nameof(UpdateRoomTypeCommand.Description), 255);
        }
    }
}
