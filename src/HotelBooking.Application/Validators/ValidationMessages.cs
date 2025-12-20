using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Validators
{
    public static class ValidationMessages
    {
        public static string Required(string fieldName) => $"{fieldName} is required";

        public static string MinLength(string fieldName, int min) => $"{fieldName} must be at least {min} characters";

        public static string MaxLength(string fieldName, int max) => $"{fieldName} must not exceed {max} characters";

        public static string LengthBetween(string fieldName, int min, int max) => $"{fieldName} must be between {min} and {max} characters";
    }
}
