using HotelBooking.Application.DTOs.AmenityDTOs;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllAmenitiesByRoomTypeIdQuery(int RoomTypeId) : IRequest<IEnumerable<AmenityDTO>>;
}
