using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class AddUserValidator : AbstractValidator<CreateUserDTO>
    {
        public AddUserValidator()
        {
            RuleFor(cu => cu.Name).NameRule();
            RuleFor(cu => cu.Phone).PhoneRule();
            RuleFor(cu => cu.Email).EmailRule();
            RuleFor(cu => cu.Password).PasswordRule();
        }
    }
}
