using HotelBooking.Application.DTOs.RoomTypeDTOs;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllRoomTypesByAmenityIdQuery(int AmenityId) : IRequest<IEnumerable<RoomTypeDTO>>;
}
