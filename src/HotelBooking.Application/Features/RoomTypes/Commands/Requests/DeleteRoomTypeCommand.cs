namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record DeleteRoomTypeCommand(int RoomTypeId) : IRequest<Result>;
}
