namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchAvailableRoomsQuery(RoomsAvailabilityFilter Filter) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}