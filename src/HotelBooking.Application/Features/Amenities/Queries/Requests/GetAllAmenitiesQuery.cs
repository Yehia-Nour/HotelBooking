using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Queries.Requests
{
    public record GetAllAmenitiesQuery(bool? IsActive) : IRequest<Result<IEnumerable<AmenityDTO>>>;
}
