using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Queries.Requests
{
    public record GetAmenityByIdQuery(int AmenityId) : IRequest<Result<AmenityDTO>>;
}
