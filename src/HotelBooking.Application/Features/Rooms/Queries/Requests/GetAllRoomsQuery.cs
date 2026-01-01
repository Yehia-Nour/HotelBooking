using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Queries.Requests
{
    public record GetAllRoomsQuery(RoomQueryParams QueryParams) : IRequest<Result<PaginatedResult<RoomDTO>>>;
}