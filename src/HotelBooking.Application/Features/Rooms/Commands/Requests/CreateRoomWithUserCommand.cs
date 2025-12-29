using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Commands.Requests
{
    public record CreateRoomWithUserCommand(CreateRoomCommand Command, string UserEmail) : IRequest<Result<int>>;
}
