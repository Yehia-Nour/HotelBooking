namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record ToggleRoomTypeActiveCommand(int RoomTypeId) : IRequest<Result>;
}
