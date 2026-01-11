using FluentValidation;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    internal class SearchRoomsByViewTypeQueryValidator : AbstractValidator<SearchRoomsByViewTypeQuery>
    {
        public SearchRoomsByViewTypeQueryValidator()
        {
            RuleFor(s => s.ViewType)
                .RequiredField(nameof(SearchRoomsByViewTypeQuery.ViewType))
                .MaxLengthField(nameof(SearchRoomsByViewTypeQuery.ViewType), 5000);
        }
    }
}
