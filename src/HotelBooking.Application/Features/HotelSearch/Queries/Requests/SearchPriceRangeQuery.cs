namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchPriceRangeQuery(RoomsPriceRangeFilter Filter) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
