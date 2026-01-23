using FluentValidation;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class SearchRoomsByMinRatingQueryValidator : AbstractValidator<SearchRoomsByMinRatingQuery>
    {
        public SearchRoomsByMinRatingQueryValidator()
        {
            RuleFor(x => x.MinRating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");
        }
    }
}