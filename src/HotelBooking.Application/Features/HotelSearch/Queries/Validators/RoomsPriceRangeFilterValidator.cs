namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class RoomsPriceRangeFilterValidator : AbstractValidator<RoomsPriceRangeFilter>
    {
        public RoomsPriceRangeFilterValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Minimum price must be zero or greater.");

            RuleFor(x => x.MaxPrice)
                .GreaterThan(0)
                .WithMessage("Maximum price must be greater than zero.");

            RuleFor(x => x)
                .Must(x => x.MaxPrice >= x.MinPrice)
                .WithMessage("Maximum price must be greater than or equal to minimum price.");
        }
    }
}