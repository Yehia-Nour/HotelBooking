namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomsByAmenityQuery(string AmenityName) : IRequest<Result<IEnumerable<RoomSearchDTO>>>;
}
