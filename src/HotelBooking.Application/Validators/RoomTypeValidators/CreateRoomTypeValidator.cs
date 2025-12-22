using FluentValidation;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.RoomTypeValidators
{
    public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeDTO>
    {
        public CreateRoomTypeValidator()
        {
            RuleFor(cr => cr.TypeName)
                .RequiredField(nameof(CreateRoomTypeDTO.TypeName))
                .MaxLengthField(nameof(CreateRoomTypeDTO.TypeName), 50);

            RuleFor(cr => cr.AccessibilityFeatures)
                .RequiredField(nameof(CreateRoomTypeDTO.AccessibilityFeatures))
                .MaxLengthField(nameof(CreateRoomTypeDTO.AccessibilityFeatures), 255);

            RuleFor(cr => cr.Description)
                .RequiredField(nameof(CreateRoomTypeDTO.Description))
                .MaxLengthField(nameof(CreateRoomTypeDTO.Description), 255);
        }
    }
}
