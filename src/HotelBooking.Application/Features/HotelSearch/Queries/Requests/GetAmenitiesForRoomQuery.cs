namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record GetAmenitiesForRoomQuery(int RoomId) : IRequest<Result<IEnumerable<AmenitySearchDTO>>>;
}
