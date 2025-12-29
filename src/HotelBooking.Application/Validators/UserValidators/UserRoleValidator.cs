using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class UserRoleValidator : AbstractValidator<UserRoleDTO>
    {
        public UserRoleValidator()
        {
            RuleFor(ur => ur.UserId).RequiredField(nameof(UserRoleDTO.UserId));
        }
    }
}
