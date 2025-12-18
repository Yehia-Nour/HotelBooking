using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiBaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var result = await _adminService.GetAllUsersAsync();

            return HandleResult(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string userId)
        {
            var result = await _adminService.GetUserByIdAsync(userId);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUserAsync(CreateUserDTO userDTO)
        {
            var result = await _adminService.AddUserAsync(userDTO);

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var result = await _adminService.UpdateUserAsync(userDTO);

            return HandleResult(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _adminService.DeleteUserAsync(userId);

            return HandleResult(result);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRoleToUser(UserRoleDTO userRoleDTO)
        {
            var result = await _adminService.AssignRoleToUserAsync(userRoleDTO);

            return HandleResult(result);
        }
    }
}
