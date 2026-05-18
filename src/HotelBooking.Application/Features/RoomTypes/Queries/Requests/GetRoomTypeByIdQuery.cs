namespace HotelBooking.Application.Features.RoomTypes.Queries.Requests
{
    public record GetRoomTypeByIdQuery(int RoomTypeId) : IRequest<Result<RoomTypeDTO>>;
}
