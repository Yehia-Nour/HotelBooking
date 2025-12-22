using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<TokenResponseDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await _authenticationService.RegisterAsync(registerDTO);

            return HandleResult(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authenticationService.LoginAsync(loginDTO);

            return HandleResult(result);
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<TokenResponseDTO>> Refresh(RefreshRequestDTO requestDTO)
        {
            var result = await _authenticationService.RefreshTokenAsync(requestDTO);
            return HandleResult(result);
        }


        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(RefreshRequestDTO requestDTO)
        {
            var result = await _authenticationService.LogoutAsync(requestDTO);

            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var userEmail = GetEmailFromToken();
            var result = await _authenticationService.ChangePasswordAsync(userEmail, passwordDTO);

            return HandleResult(result);
        }
    }
}
