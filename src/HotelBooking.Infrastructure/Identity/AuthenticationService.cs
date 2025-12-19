using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Results;
using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Infrastructure.Identity.Entities;
using HotelBooking.Infrastructure.Identity.Security;
using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Infrastructure.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtService _jwtService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IRefreshTokenService refreshTokenService,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
            _jwtService = jwtService;
        }

        public async Task<Result<TokenResponseDTO>> RegisterAsync(RegisterDTO registerDTO)
        {

            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Name,
                PhoneNumber = registerDTO.Phone
            };

            var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
                return identityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

            var roleResult = await _userManager.AddToRoleAsync(user, Role.Guest.ToString());
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Error.Failure("RoleAssignmentFailed", "User Created But Role Assignment Failed");
            }

            var accessToken = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<Result<TokenResponseDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                return Error.InvalidCrendentials("User.InvalidCrendentials");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
                return Error.InvalidCrendentials("User.InvalidCrendentials");

            var accessToken = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<Result<TokenResponseDTO>> RefreshTokenAsync(RefreshRequestDTO requestDTO)
        {
            var userId = await _refreshTokenService.GetUserIdFromValidRefreshTokenAsync(requestDTO.RefreshToken);

            if (userId is null)
                return Error.InvalidCrendentials("User.InvalidCrendentials", "Refresh Token Is Invalid");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Error.InvalidCrendentials("User.InvalidCrendentials");

            await _refreshTokenService.RevokeRefreshTokenAsync(requestDTO.RefreshToken);

            var newAccessToken = await _jwtService.GenerateTokenAsync(user);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<Result> LogoutAsync(string refreshToken)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);
            return Result.Ok();
        }

        public async Task<Result> ChangePasswordAsync(string userEmail, ChangePasswordDTO passwordDTO)
        {
            if (passwordDTO.NewPassword != passwordDTO.ConfirmNewPassword)
                return Result.Fail(Error.InvalidCrendentials("User.InvalidCrendentials", "New Password And Confirmation Password Do Not Mtch"));

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return Result.Fail(Error.Unauthorized("User.Unauthorized"));

            var result = await _userManager.ChangePasswordAsync(user, passwordDTO.CurrentPassword, passwordDTO.NewPassword);
            if (!result.Succeeded)
                return Result.Fail(result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList());

            return Result.Ok();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }
    }
}
