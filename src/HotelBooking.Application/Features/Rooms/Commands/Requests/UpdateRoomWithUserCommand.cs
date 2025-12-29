using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public record UpdateRoomWithUserCommand(UpdateRoomCommand Command, string UserEmail) : IRequest<Result>;
}
