namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class SearchRoomsByAmenityQueryValidator : AbstractValidator<SearchRoomsByAmenityQuery>
    {
        public SearchRoomsByAmenityQueryValidator()
        {
            RuleFor(s => s.AmenityName)
                .RequiredField(nameof(SearchRoomsByAmenityQuery.AmenityName))
                .MaxLengthField(nameof(SearchRoomsByAmenityQuery.AmenityName), 100);
        }
    }
}