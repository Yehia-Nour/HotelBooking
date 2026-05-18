namespace HotelBooking.Presentation.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authenticationService)
        {
            _authService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<TokenResponseDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);

            return HandleResult(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);

            return HandleResult(result);
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<TokenResponseDTO>> Refresh(RefreshRequestDTO requestDTO)
        {
            var result = await _authService.RefreshTokenAsync(requestDTO);
            return HandleResult(result);
        }


        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(RefreshRequestDTO requestDTO)
        {
            var result = await _authService.LogoutAsync(requestDTO);

            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var userEmail = GetEmailFromToken();
            var result = await _authService.ChangePasswordAsync(userEmail, passwordDTO);

            return HandleResult(result);
        }

        [HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action(nameof(GoogleCallback), "Auth");

            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleCallback")]
        public async Task<ActionResult<TokenResponseDTO>> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return Unauthorized();

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var name = result.Principal.FindFirstValue(ClaimTypes.Name);
            var providerKey = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email not found from Google.");

            if (string.IsNullOrWhiteSpace(providerKey))
                return BadRequest("Google user identifier not found.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var tokenResult = await _authService.GoogleLoginAsync(email, name ?? email, providerKey);

            return HandleResult(tokenResult);
        }
    }
}
