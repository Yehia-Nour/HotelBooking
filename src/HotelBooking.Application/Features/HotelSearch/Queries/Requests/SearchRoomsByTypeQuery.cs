namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsByTypeQuery(string RoomTypeName) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
