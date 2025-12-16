using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator(IAuthenticationService authService)
        {
            RuleFor(ru => ru.Name).NameRule(); 
            RuleFor(ru => ru.Phone).PhoneRule();
            RuleFor(ru => ru.Email).EmailRule();
            RuleFor(ru => ru.Password).PasswordRule();
        }
    }
}
