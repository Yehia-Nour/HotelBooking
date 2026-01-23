using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public record AddGuestsToReservationWithUserCommand(string UserId, string UserEmail, AddGuestsToReservationCommand Command) : IRequest<Result>;
}
