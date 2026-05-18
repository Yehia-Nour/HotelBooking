namespace HotelBooking.Application.Features.HotelSearch.Queries.Requests
{
    public record SearchRoomDetailsWithAmenitiesQuery(int RoomId) : IRequest<Result<RoomSearchWithAmenitiesDTO>>;
}
