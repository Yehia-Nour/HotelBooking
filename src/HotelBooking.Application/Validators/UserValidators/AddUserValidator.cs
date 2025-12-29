using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class AddUserValidator : AbstractValidator<CreateUserDTO>
    {
        public AddUserValidator()
        {
            RuleFor(cu => cu.Name)
                .RequiredField(nameof(CreateUserDTO.Name))
                .LengthBetweenField(nameof(CreateUserDTO.Name), 2, 50)
                .LettersOnlyField(nameof(CreateUserDTO.Name));

            RuleFor(cu => cu.Phone)
                .RequiredField(nameof(CreateUserDTO.Phone))
                .PhoneField(nameof(CreateUserDTO.Phone));

            RuleFor(cu => cu.Email)
                .RequiredField(nameof(CreateUserDTO.Email))
                .LengthBetweenField(nameof(CreateUserDTO.Email), 10, 50)
                .EmailField(nameof(CreateUserDTO.Email));

            RuleFor(cu => cu.Password)
                .RequiredField(nameof(CreateUserDTO.Password))
                .MinLengthField(nameof(CreateUserDTO.Password), 8)
                .PasswordField(nameof(CreateUserDTO.Password));
        }
    }
}
