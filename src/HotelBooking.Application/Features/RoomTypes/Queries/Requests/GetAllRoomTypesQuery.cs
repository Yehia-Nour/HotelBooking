using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Queries.Requests
{
    public record GetAllRoomTypesQuery(bool? IsActive) : IRequest<Result<IEnumerable<RoomTypeDTO>>>;
}
