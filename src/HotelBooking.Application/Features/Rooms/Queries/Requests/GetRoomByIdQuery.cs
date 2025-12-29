using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Queries.Requests
{
    public record GetRoomByIdQuery(int Id) : IRequest<Result<RoomDTO>>;
}
