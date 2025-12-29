using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record ToggleRoomTypeActiveCommand(int RoomTypeId) : IRequest<Result>;
}
