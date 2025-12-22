using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators
{

    public static class ValidationRules
    {
        public static IRuleBuilderOptions<T, string> RequiredField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder.NotEmpty().WithMessage($"{fieldName} is required");
        }

        public static IRuleBuilderOptions<T, string> MinLengthField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int min)
        {
            return ruleBuilder.MinimumLength(min).WithMessage($"{fieldName} must be at least {min} characters");
        }

        public static IRuleBuilderOptions<T, string> MaxLengthField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int max)
        {
            return ruleBuilder.MaximumLength(max).WithMessage($"{fieldName} must not exceed {max} characters");
        }

        public static IRuleBuilderOptions<T, string> LengthBetweenField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName, int min, int max)
        {
            return ruleBuilder.Length(min, max).WithMessage($"{fieldName} must be between {min} and {max} characters");
        }

        public static IRuleBuilderOptions<T, string> LettersOnlyField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder.Matches(@"^[a-zA-Z\s]+$").WithMessage($"{fieldName} can contain only letters");
        }

        public static IRuleBuilderOptions<T, int> RequiredNumberField<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .GreaterThan(0)
                    .WithMessage($"{fieldName} must be greater than 0");
        }

        public static IRuleBuilderOptions<T, string> EmailField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .EmailAddress().WithMessage($"{fieldName} must be a valid email address");
        }

        public static IRuleBuilderOptions<T, string> PhoneField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder.Matches(@"^(010|011|012|015)\d{8}$").WithMessage($"{fieldName} must be a valid phone number");
        }

        public static IRuleBuilderOptions<T, string> PasswordField<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .Matches(@"[A-Z]+").WithMessage($"{fieldName} must contain at least one uppercase letter")
                .Matches(@"[a-z]+").WithMessage($"{fieldName} must contain at least one lowercase letter")
                .Matches(@"\d+").WithMessage($"{fieldName} must contain at least one number")
                .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]+").WithMessage($"{fieldName} must contain at least one special character (!@#$%^&*()-+=)");
        }
    }
}
