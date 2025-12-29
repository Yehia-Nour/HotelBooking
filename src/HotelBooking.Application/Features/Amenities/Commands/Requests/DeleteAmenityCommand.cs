using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Commands.Requests
{
    public record DeleteAmenityCommand(int AmenityId) : IRequest<Result>;
}