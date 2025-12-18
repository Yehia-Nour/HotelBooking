using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Results;
using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Identity
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.UserName,
                    Phone = u.PhoneNumber
                }).ToListAsync();

            return users;
        }

        public async Task<Result<UserDTO>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                return Error.NotFound("User.NotFound", $"User With Id {userId} Not Found");

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Phone = user.PhoneNumber
            };
        }

        public async Task<Result<UserDTO>> AddUserAsync(CreateUserDTO userDTO)
        {
            var user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Name,
                PhoneNumber = userDTO.Phone,
            };

            var identityResult = await _userManager.CreateAsync(user, userDTO.Password);
            if (!identityResult.Succeeded)
                return identityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

            var roleResult = await _userManager.AddToRoleAsync(user, Role.Guest.ToString());
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Error.Failure("RoleAssignmentFailed", "User created but role assignment failed");
            }

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Phone = user.PhoneNumber
            };
        }

        public async Task<Result<UserDTO>> UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user is null)
                return Error.NotFound("User.NotFound", $"User With Id {userDTO.Id} Not Found");

            user.UserName = userDTO.Name;
            user.PhoneNumber = userDTO.Phone;
            user.Email = userDTO.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Phone = user.PhoneNumber
            };
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                return Result.Fail(Error.NotFound("User.NotFound", $"User With Id {userId} Not Found"));

            var roleForUser = await _userManager.GetRolesAsync(user);
            if (roleForUser.Contains("Admin"))
                return Result.Fail(Error.Forbidden("Admin.SelfDelete", "Admin Cannot Delete Himself"));

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return Result.Fail(Error.Failure("user.Failure", $"User With Id {userId} Cant't Be Delete"));

            return Result.Ok();
        }

        public async Task<Result> AssignRoleToUserAsync(UserRoleDTO userRoleDTO)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userRoleDTO.UserId);
            if (user is null)
                return Result.Fail(Error.NotFound("User.NotFound", $"User with Id {userRoleDTO.UserId} not found"));

            var roleName = userRoleDTO.Role.ToString();

            var existingRoles = await _userManager.GetRolesAsync(user);
            if (existingRoles.Contains("Admin"))
                return Result.Fail(Error.Validation( "Role.AssignmentFailed", "Cannot assign a new role because the user is an Admin"));

            if (existingRoles.Contains(roleName))
                return Result.Fail(Error.Validation("Role.Exists", $"User already has role '{roleName}'"));

            foreach (var role in existingRoles)
                await _userManager.RemoveFromRoleAsync(user, role);

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                return Result.Fail(result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList());

            return Result.Ok();
        }
    }
}
