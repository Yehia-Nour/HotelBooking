using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Queries.Requests
{
    public record GetAllRoomsQuery(RoomQueryParams QueryParams) : IRequest<Result<PaginatedResult<RoomDTO>>>;
}
