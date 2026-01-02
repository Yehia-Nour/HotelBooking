using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllAmenitiesByRoomTypeIdQuery(int RoomTypeId) : IRequest<Result<IEnumerable<AmenityDTO>>>;
}
