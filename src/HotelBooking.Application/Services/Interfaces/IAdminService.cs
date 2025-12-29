using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Results;

namespace HotelBooking.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Result<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<Result<UserDTO>> GetUserByIdAsync(string userId);
        Task<Result<UserDTO>> AddUserAsync(CreateUserDTO userDTO);
        Task<Result<UserDTO>> UpdateUserAsync(UpdateUserDTO userDTO);
        Task<Result> DeleteUserAsync(string userId);
        Task<Result> AssignRoleToUserAsync(UserRoleDTO userRoleDTO);
    }
}
