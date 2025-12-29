using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(ch => ch.CurrentPassword).RequiredField(nameof(ChangePasswordDTO.CurrentPassword));

            RuleFor(ch => ch.NewPassword)
                .RequiredField(nameof(ChangePasswordDTO.NewPassword))
                .MinLengthField(nameof(ChangePasswordDTO.NewPassword), 8)
                .PasswordField(nameof(ChangePasswordDTO.NewPassword));

            RuleFor(ch => ch.ConfirmNewPassword).RequiredField(nameof(ChangePasswordDTO.ConfirmNewPassword));
        }
    }
}
