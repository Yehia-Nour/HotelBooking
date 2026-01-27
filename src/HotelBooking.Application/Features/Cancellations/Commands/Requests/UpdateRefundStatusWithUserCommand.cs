using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Refunds.Commands.Requests
{
    public record UpdateRefundStatusWithUserCommand(string UserId, UpdateRefundStatusCommand Command) : IRequest<Result>;
}
