using FluentValidation;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.RoomTypeValidators
{
    internal class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeDTO>
    {
        public UpdateRoomTypeValidator()
        {
            RuleFor(ur => ur.RoomTypeId).RequiredNumberField(nameof(UpdateRoomTypeDTO.RoomTypeId));

            RuleFor(ur => ur.TypeName)
                .RequiredField(nameof(UpdateRoomTypeDTO.TypeName))
                .MaxLengthField(nameof(UpdateRoomTypeDTO.TypeName), 50);


            RuleFor(ur => ur.AccessibilityFeatures)
                .RequiredField(nameof(UpdateRoomTypeDTO.AccessibilityFeatures))
                .MaxLengthField(nameof(UpdateRoomTypeDTO.AccessibilityFeatures), 255);

            RuleFor(ur => ur.Description)
                .RequiredField(nameof(UpdateRoomTypeDTO.Description))
                .MaxLengthField(nameof(UpdateRoomTypeDTO.Description), 255);
        }
    }
}
