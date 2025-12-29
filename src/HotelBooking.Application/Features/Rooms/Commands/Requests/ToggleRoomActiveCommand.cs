using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public record ToggleRoomActiveCommand(int RoomId) : IRequest<Result>;
}
