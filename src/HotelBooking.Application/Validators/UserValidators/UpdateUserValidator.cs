using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(uu => uu.Name).NameRule();
            RuleFor(uu => uu.Phone).PhoneRule();
            RuleFor(uu => uu.Email).EmailRule();
        }
    }
}
