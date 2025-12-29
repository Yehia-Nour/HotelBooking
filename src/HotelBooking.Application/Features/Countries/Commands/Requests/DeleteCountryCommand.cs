using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public record DeleteCountryCommand(int CountryId) : IRequest<Result>;
}
