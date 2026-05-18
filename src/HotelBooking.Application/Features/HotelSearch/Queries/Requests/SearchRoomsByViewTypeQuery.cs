namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsByViewTypeQuery(string ViewType) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
