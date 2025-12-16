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
                    .WithMessage("Name is Required")
                .Length(2, 50)
                    .WithMessage("Name Must be Between 2 and 50 Characters")
                .Matches(@"^[a-zA-Z\s]+$")
                    .WithMessage("Name Can Contain Only Letters");
        }

        public static IRuleBuilderOptions<T, string> PhoneRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage("Phone is Required")
                .Matches(@"^(010|011|012|015)\d{8}$")
                    .WithMessage("Phone Number Must be Valid Egyptian Phone Number");
        }

        public static IRuleBuilderOptions<T, string> EmailRule<T>(this IRuleBuilder<T, string> ruleBuilder, IAuthenticationService authService)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage("Email is Required")
                .EmailAddress()
                    .WithMessage("Invalid Email Format")
                .Length(5, 100).WithMessage("Email Must be Between 5 and 100 Characters")
                .MustAsync(async (email, ct) => !await authService.EmailExistsAsync(email))
                    .WithMessage("Email already exists");
        }

        public static IRuleBuilderOptions<T, string> PasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                    .WithMessage("Password is Required")
                .MinimumLength(8)
                    .WithMessage("Password Must be at least 8 Characters")
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
