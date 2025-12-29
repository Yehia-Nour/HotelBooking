using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public record CreateAmenityWithUserCommand(CreateAmenityCommand Command, string UserEmail) : IRequest<Result<int>>;
}
