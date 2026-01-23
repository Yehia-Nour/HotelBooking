using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.HotelBooking.Commands.Requests
{
    public record ProcessPaymentWithUserCommand(string UserId, ProcessPaymentCommand Command) : IRequest<Result<int>>;
}
