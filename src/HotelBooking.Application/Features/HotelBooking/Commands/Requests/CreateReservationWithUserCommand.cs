using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public record CreateReservationWithUserCommand(CreateReservationCommand Command, string UserId) : IRequest<Result<int>>;
}