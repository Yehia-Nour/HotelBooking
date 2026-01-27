using FluentValidation;
using HotelBooking.Application.Features.Cancellations.Queries.Requests;

namespace HotelBooking.Application.Features.Cancellations.Queries.Validators
{
    public class GetAllCancellationsRequestValidator : AbstractValidator<GetAllCancellationsRequest>
    {
        public GetAllCancellationsRequestValidator()
        {
            RuleFor(x => x)
                .Must(x =>
                    !(x.DateFrom.HasValue && x.DateTo.HasValue) ||
                    x.DateTo.Value >= x.DateFrom.Value)
                .WithMessage("DateTo must be greater than or equal to DateFrom.");
        }
    }
}
