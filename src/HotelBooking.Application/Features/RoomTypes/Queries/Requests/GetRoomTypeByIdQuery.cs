using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Queries.Requests
{
    public record GetRoomTypeByIdQuery(int RoomTypeId) : IRequest<Result<RoomTypeDTO>>;
}
