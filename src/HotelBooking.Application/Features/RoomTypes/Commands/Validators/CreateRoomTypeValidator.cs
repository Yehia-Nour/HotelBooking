using FluentValidation;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Features.RoomTypes.Commands.Requests;
using HotelBooking.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Validators
{
    public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeValidator()
        {
            RuleFor(cr => cr.TypeName)
                .RequiredField(nameof(CreateRoomTypeCommand.TypeName))
                .MaxLengthField(nameof(CreateRoomTypeCommand.TypeName), 50);

            RuleFor(cr => cr.AccessibilityFeatures)
                .RequiredField(nameof(CreateRoomTypeCommand.AccessibilityFeatures))
                .MaxLengthField(nameof(CreateRoomTypeCommand.AccessibilityFeatures), 255);

            RuleFor(cr => cr.Description)
                .RequiredField(nameof(CreateRoomTypeCommand.Description))
                .MaxLengthField(nameof(CreateRoomTypeCommand.Description), 255);
        }
    }
}
