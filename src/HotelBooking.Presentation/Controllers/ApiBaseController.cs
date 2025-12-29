using HotelBooking.Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace HotelBooking.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);
        }

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (!result.IsSuccess)
                return HandleProblem(result.Errors);

            return Ok(result.Value);
        }

        protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error Occured");

            if (errors.All(e => e.Type == ErrorType.Validation))
                return HandleValidationProblem(errors);

            return HandleSingleErrorProblem(errors[0]);
        }

        private ActionResult HandleSingleErrorProblem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Type.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.Type)
                );
        }

        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };


        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
                modelState.AddModelError(error.Code, error.Description);

            return ValidationProblem(modelState);
        }
    }
}
