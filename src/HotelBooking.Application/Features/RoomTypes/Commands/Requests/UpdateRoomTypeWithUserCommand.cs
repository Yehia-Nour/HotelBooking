namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record UpdateRoomTypeWithUserCommand(UpdateRoomTypeCommand Command, string UserEmail) : IRequest<Result>;
}
