using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Name)
                .RequiredField(nameof(CreateUserDTO.Name))
                .LengthBetweenField(nameof(CreateUserDTO.Name), 2, 50)
                .LettersOnlyField(nameof(CreateUserDTO.Name));

            RuleFor(u => u.Phone)
                .RequiredField(nameof(CreateUserDTO.Phone))
                .PhoneField(nameof(CreateUserDTO.Phone));

            RuleFor(u => u.Email)
                .RequiredField(nameof(CreateUserDTO.Email))
                .LengthBetweenField(nameof(CreateUserDTO.Email), 10, 50)
                .EmailField(nameof(CreateUserDTO.Email));
        }
    }
}
