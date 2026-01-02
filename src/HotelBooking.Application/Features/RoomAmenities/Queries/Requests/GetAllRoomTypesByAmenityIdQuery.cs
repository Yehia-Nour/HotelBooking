using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Requests
{
    public record GetAllRoomTypesByAmenityIdQuery(int AmenityId) : IRequest<Result<IEnumerable<RoomTypeDTO>>>;
}
