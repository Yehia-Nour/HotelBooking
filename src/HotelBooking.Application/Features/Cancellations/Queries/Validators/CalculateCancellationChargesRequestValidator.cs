using FluentValidation;
using HotelBooking.Application.Features.Cancellations.Queries.Requests;

namespace HotelBooking.Application.Features.Cancellations.Queries.Validators
{
    public class CalculateCancellationChargesRequestValidator : AbstractValidator<CalculateCancellationChargesRequest>
    {
        public CalculateCancellationChargesRequestValidator()
        {
            RuleFor(x => x.ReservationId)
                .GreaterThan(0)
                .WithMessage("ReservationId must be a positive integer.");

            RuleFor(x => x.RoomsCancelled)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("At least one roomId is required.");

            RuleForEach(x => x.RoomsCancelled)
                .GreaterThan(0)
                .WithMessage("RoomId must be a positive integer.");
        }
    }
}
