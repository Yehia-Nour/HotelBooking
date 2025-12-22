using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Results
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        public static Error Failure(string code = "General.Failure", string description = "A General Failure Has Occured")
            => new(code, description, ErrorType.Failure);

        public static Error Validation(string code = "General.Validation", string description = "Validation Error Has Occured")
            => new(code, description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound", string description = "The Requested Resource Was NotFound")
            => new(code, description, ErrorType.NotFound);

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "You Are Not Authorized")
            => new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code = "General.Forbidden", string description = "You Do Not Have Permission")
            => new(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "The Provided Credentials Are Invalid")
            => new(code, description, ErrorType.InvalidCredentials);
    }
}
