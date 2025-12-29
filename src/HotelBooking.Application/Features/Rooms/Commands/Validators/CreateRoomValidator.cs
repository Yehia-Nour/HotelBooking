using FluentValidation;
using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.Rooms.Commands.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidator()
        {
            RuleFor(cr => cr.RoomNumber)
                .RequiredField(nameof(CreateRoomCommand.RoomNumber))
                .MaxLengthField(nameof(CreateRoomCommand.RoomNumber), 10)
                .NumbersOnlyField(nameof(CreateRoomCommand.RoomNumber));

            RuleFor(cr => cr.RoomTypeID).RequiredNumberField(nameof(CreateRoomCommand.RoomTypeID));

            RuleFor(cr => cr.Price).PriceField(nameof(CreateRoomCommand.Price));

            RuleFor(cr => cr.BedType)
                .RequiredField(nameof(CreateRoomCommand.BedType))
                .MaxLengthField(nameof(CreateRoomCommand.BedType), 50);

            RuleFor(cr => cr.ViewType)
                .RequiredField(nameof(CreateRoomCommand.ViewType))
                .MaxLengthField(nameof(CreateRoomCommand.ViewType), 50);

            RuleFor(cr => cr.Status).EnumField(nameof(CreateRoomCommand.Status));
        }
    }
}
