using FluentValidation;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class SearchRoomsByTypeQueryValidator : AbstractValidator<SearchRoomsByTypeQuery>
    {
        public SearchRoomsByTypeQueryValidator()
        {
            RuleFor(s => s.RoomTypeName)
                .RequiredField(nameof(SearchRoomsByTypeQuery.RoomTypeName))
                .MaxLengthField(nameof(SearchRoomsByTypeQuery.RoomTypeName), 100);
        }
    }
}