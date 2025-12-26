using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<Result<PaginatedResult<RoomDTO>>> GetAllRoomsAsync(RoomQueryParams queryParams);
    }
}
