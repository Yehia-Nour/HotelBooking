using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record CreateRoomTypeWithUserCommand(CreateRoomTypeCommand Command, string UserEmail) : IRequest<Result<int>>;

}
