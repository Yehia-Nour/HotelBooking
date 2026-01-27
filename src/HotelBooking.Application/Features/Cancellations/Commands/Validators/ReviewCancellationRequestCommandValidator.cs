using FluentValidation;
using HotelBooking.Application.Features.Cancellations.Commands.Requests;
using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.Features.Cancellations.Commands.Validators
{
    public class ReviewCancellationRequestCommandValidator : AbstractValidator<ReviewCancellationRequestCommand>
    {
        public ReviewCancellationRequestCommandValidator()
        {
            RuleFor(x => x.CancellationRequestId)
                .GreaterThan(0);

            RuleFor(x => x.ApprovalStatus)
                .Must(s => s == CancellationStatus.Approved || s == CancellationStatus.Denied)
                .WithMessage("ApprovalStatus must be Approved or Denied.");
        }
    }
}
