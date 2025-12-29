using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Name)
                .RequiredField(nameof(CreateUserDTO.Name))
                .LengthBetweenField(nameof(CreateUserDTO.Name), 2, 50)
                .LettersOnlyField(nameof(CreateUserDTO.Name));

            RuleFor(r => r.Phone)
                .RequiredField(nameof(CreateUserDTO.Phone))
                .PhoneField(nameof(CreateUserDTO.Phone));

            RuleFor(r => r.Email)
                .RequiredField(nameof(CreateUserDTO.Email))
                .LengthBetweenField(nameof(CreateUserDTO.Email), 10, 50)
                .EmailField(nameof(CreateUserDTO.Email));

            RuleFor(r => r.Password)
                .RequiredField(nameof(CreateUserDTO.Password))
                .MinLengthField(nameof(CreateUserDTO.Password), 8)
                .PasswordField(nameof(CreateUserDTO.Password));
        }
    }
}
