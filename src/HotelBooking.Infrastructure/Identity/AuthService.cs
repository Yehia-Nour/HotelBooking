namespace HotelBooking.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager,
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
                return Error.InvalidCredentials("User.InvalidCrendentials");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
                return Error.InvalidCredentials("User.InvalidCrendentials");

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
                return Error.InvalidCredentials("User.InvalidCrendentials", "Refresh Token Is Invalid");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Error.InvalidCredentials("User.InvalidCrendentials");

            await _refreshTokenService.RevokeRefreshTokenAsync(requestDTO.RefreshToken);

            var newAccessToken = await _jwtService.GenerateTokenAsync(user);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<Result> LogoutAsync(RefreshRequestDTO requestDTO)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(requestDTO.RefreshToken);
            return Result.Ok();
        }

        public async Task<Result> ChangePasswordAsync(string userEmail, ChangePasswordDTO passwordDTO)
        {
            if (passwordDTO.NewPassword != passwordDTO.ConfirmNewPassword)
                return Result.Fail(Error.InvalidCredentials("User.InvalidCrendentials", "New Password And Confirmation Password Do Not Mtch"));

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

        public async Task<Result<TokenResponseDTO>> GoogleLoginAsync(string email, string name, string providerKey)
        {
            const string provider = "Google";

            var user = await _userManager.FindByLoginAsync(provider, providerKey);

            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(email);
            }

            if (user is null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                var identityResult = await _userManager.CreateAsync(user);

                if (!identityResult.Succeeded)
                    return identityResult.Errors
                        .Select(e => Error.Validation(e.Code, e.Description))
                        .ToList();

                var roleResult = await _userManager.AddToRoleAsync(user, Role.Guest.ToString());

                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return Error.Failure(
                        "RoleAssignmentFailed",
                        "User Created But Role Assignment Failed");
                }
            }

            var existingLogins = await _userManager.GetLoginsAsync(user);
            if (!existingLogins.Any(login =>
                    login.LoginProvider == provider &&
                    login.ProviderKey == providerKey))
            {
                var loginResult = await _userManager.AddLoginAsync(
                    user,
                    new UserLoginInfo(provider, providerKey, name));

                if (!loginResult.Succeeded)
                    return loginResult.Errors
                        .Select(e => Error.Validation(e.Code, e.Description))
                        .ToList();
            }

            var accessToken = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
