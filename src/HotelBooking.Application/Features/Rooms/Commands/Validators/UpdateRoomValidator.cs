using FluentValidation;
using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Commands.Validators
{
    internal class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomValidator()
        {
            RuleFor(cr => cr.RoomNumber)
                .RequiredField(nameof(UpdateRoomCommand.RoomNumber))
                .MaxLengthField(nameof(UpdateRoomCommand.RoomNumber), 10)
                .NumbersOnlyField(nameof(UpdateRoomCommand.RoomNumber));

            RuleFor(cr => cr.RoomTypeID).RequiredNumberField(nameof(UpdateRoomCommand.RoomTypeID));

            RuleFor(cr => cr.Price).PriceField(nameof(UpdateRoomCommand.Price));

            RuleFor(cr => cr.BedType)
                .RequiredField(nameof(UpdateRoomCommand.BedType))
                .MaxLengthField(nameof(UpdateRoomCommand.BedType), 50);

            RuleFor(cr => cr.ViewType)
                .RequiredField(nameof(UpdateRoomCommand.ViewType))
                .MaxLengthField(nameof(UpdateRoomCommand.ViewType), 50);

            RuleFor(cr => cr.Status).EnumField(nameof(UpdateRoomCommand.Status));
        }
    }
}
