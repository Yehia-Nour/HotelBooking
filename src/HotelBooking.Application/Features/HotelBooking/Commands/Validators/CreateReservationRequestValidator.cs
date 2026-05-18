namespace HotelBooking.Application.Features.HotelBooking.Commands.Validators
{
    public class CreateReservationRequestValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationRequestValidator()
        {
            RuleFor(x => x.RoomIds)
                .NotNull()
                .Must(ids => ids.Count > 0)
                .WithMessage("At least one roomId is required.");

            RuleForEach(x => x.RoomIds)
                .GreaterThan(0);

            RuleFor(x => x.CheckInDate).NotNull();
            RuleFor(x => x.CheckOutDate).NotNull();

            RuleFor(x => x)
                .Must(x => x.CheckInDate.HasValue && x.CheckOutDate.HasValue
                           && x.CheckOutDate.Value.Date > x.CheckInDate.Value.Date)
                .WithMessage("Check-out date must be after check-in date.");
        }
    }
}
