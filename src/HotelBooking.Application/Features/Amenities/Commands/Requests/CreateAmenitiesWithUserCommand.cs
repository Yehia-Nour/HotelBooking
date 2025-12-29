using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public record CreateAmenitiesWithUserCommand(CreateAmenitiesCommand Command, string UserEmail) : IRequest<Result<int>>;
}
