using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.States.Commands.Requests
{
    public record CreateStateWithUserCommand(CreateStateCommand Command, string UserEmail) : IRequest<Result<int>>;
}
