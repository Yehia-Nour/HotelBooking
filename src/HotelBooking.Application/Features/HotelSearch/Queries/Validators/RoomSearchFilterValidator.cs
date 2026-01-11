using FluentValidation;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    internal class RoomSearchFilterValidator : AbstractValidator<RoomSearchFilter>
    {
        public RoomSearchFilterValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x =>
                    !x.MinPrice.HasValue
                    || !x.MaxPrice.HasValue
                    || x.MinPrice <= x.MaxPrice)
                .WithMessage("MinPrice must be less than or equal to MaxPrice.");

            RuleFor(x => x.RoomTypeName)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.RoomTypeName));

            RuleFor(x => x.AmenityName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.AmenityName));

            RuleFor(x => x.ViewType)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.ViewType));
        }
    }
}
