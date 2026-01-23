using FluentValidation;
using HotelBooking.Application.Features.HotelBooking.Queries.Requests;

namespace HotelBooking.Application.Features.HotelBooking.Queries.Validators
{
    public class CalculateRoomCostRequestValidator : AbstractValidator<CalculateRoomCostRequest>
    {
        public CalculateRoomCostRequestValidator()
        {
            RuleFor(x => x.RoomIds)
                .NotNull()
                .Must(ids => ids.Count > 0)
                .WithMessage("At least one roomId is required.");

            RuleForEach(x => x.RoomIds)
                .GreaterThan(0)
                .WithMessage("RoomId must be a positive integer.");

            RuleFor(x => x.CheckInDate)
                .NotNull().WithMessage("Check-in date is required.");

            RuleFor(x => x.CheckOutDate)
                .NotNull().WithMessage("Check-out date is required.");

            RuleFor(x => x)
                .Must(x => x.CheckInDate.HasValue
                           && x.CheckOutDate.HasValue
                           && x.CheckOutDate.Value.Date > x.CheckInDate.Value.Date)
                .WithMessage("Check-out date must be after check-in date.");
        }
    }
}
