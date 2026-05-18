namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsCustomQuery(RoomSearchFilter Filter) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
