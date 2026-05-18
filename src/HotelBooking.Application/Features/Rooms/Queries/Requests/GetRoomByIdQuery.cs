namespace HotelBooking.Application.Features.Rooms.Queries.Requests
{
    public record GetRoomByIdQuery(int Id) : IRequest<Result<RoomDTO>>;
}
