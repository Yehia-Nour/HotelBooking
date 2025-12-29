using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email).RequiredField(nameof(LoginDTO.Email));

            RuleFor(l => l.Password).RequiredField(nameof(LoginDTO.Password));
        }
    }
}
