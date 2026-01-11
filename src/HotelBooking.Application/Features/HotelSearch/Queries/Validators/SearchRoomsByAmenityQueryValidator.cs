using FluentValidation;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using HotelBooking.Application.Validators;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    internal class SearchRoomsByAmenityQueryValidator : AbstractValidator<SearchRoomsByAmenityQuery>
    {
        public SearchRoomsByAmenityQueryValidator()
        {
            RuleFor(s => s.AmenityName)
                .RequiredField(nameof(SearchRoomsByAmenityQuery.AmenityName))
                .MaxLengthField(nameof(SearchRoomsByAmenityQuery.AmenityName), 100);
        }
    }
}