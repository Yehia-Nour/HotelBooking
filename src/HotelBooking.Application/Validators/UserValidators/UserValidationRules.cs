using FluentValidation;
using HotelBooking.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators.UserValidators
{
    public static class UserValidationRules
    {
        public static IRuleBuilderOptions<T, string> NameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage(ValidationMessages.Required("Name"))
                .Length(2, 50)
                    .WithMessage(ValidationMessages.LengthBetween("Name", 2, 50))
                .Matches(@"^[a-zA-Z\s]+$")
                    .WithMessage("Name Can Contain Only Letters");
        }

        public static IRuleBuilderOptions<T, string> PhoneRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage(ValidationMessages.Required("Phone"))
                .Matches(@"^(010|011|012|015)\d{8}$")
                    .WithMessage("Phone Number Must be Valid Egyptian Phone Number");
        }

        public static IRuleBuilderOptions<T, string> EmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage(ValidationMessages.Required("Email"))
                .EmailAddress()
                    .WithMessage("Invalid Email Format")
                .Length(5, 100).WithMessage(ValidationMessages.LengthBetween("Email", 10, 100));
        }

        public static IRuleBuilderOptions<T, string> PasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage(ValidationMessages.Required("Password"))
                .MinimumLength(8)
                    .WithMessage(ValidationMessages.MinLength("Password", 8))
                .Matches(@"[A-Z]+")
                    .WithMessage("Password Must contain at least one Uppercase Letter")
                .Matches(@"[a-z]+")
                    .WithMessage("Password Must contain at least one Lowercase Letter")
                .Matches(@"\d+")
                    .WithMessage("Password Must contain at least one Number")
                .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]+")
                    .WithMessage("Password Must contain at least one Special Character (!@#$%^&*()-+=)");
        }
    }
}
