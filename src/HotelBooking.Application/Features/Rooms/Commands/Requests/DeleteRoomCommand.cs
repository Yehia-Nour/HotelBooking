using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public record DeleteRoomCommand(int RoomId) : IRequest<Result>;
}
