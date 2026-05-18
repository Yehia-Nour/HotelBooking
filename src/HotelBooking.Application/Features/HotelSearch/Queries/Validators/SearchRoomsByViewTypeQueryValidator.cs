namespace HotelBooking.Application.Features.HotelSearch.Queries.Validators
{
    public class SearchRoomsByViewTypeQueryValidator : AbstractValidator<SearchRoomsByViewTypeQuery>
    {
        public SearchRoomsByViewTypeQueryValidator()
        {
            RuleFor(s => s.ViewType)
                .RequiredField(nameof(SearchRoomsByViewTypeQuery.ViewType))
                .MaxLengthField(nameof(SearchRoomsByViewTypeQuery.ViewType), 5000);
        }
    }
}
