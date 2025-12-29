using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public record CreateCountryWithUserCommand(CreateCountryCommand Command, string UserEmail) : IRequest<Result<int>>;
}
