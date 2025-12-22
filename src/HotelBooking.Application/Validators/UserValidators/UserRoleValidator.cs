using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
