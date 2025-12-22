using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<Result<IEnumerable<RoomTypeDTO>>> GetAllRoomTypesAsync(bool? IsActive);
        Task<Result<RoomTypeDTO>> GetRoomTypeByIdAsync(int id);
        Task<Result<RoomTypeDTO>> CreateRoomTypeAsync(string userEmail, CreateRoomTypeDTO roomTypeDTO);
        Task<Result> UpdateRoomTypeAsync(string userEmail, UpdateRoomTypeDTO roomTypeDTO);
        Task<Result> DeleteRoomTypeAsync(int roomTypeId);
        Task<Result> ToggleRoomTypeActiveAsync(int roomTypeId);
    }
}
