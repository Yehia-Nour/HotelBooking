namespace HotelBooking.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<TokenResponseDTO>> RegisterAsync(RegisterDTO registerDTO);
        Task<Result<TokenResponseDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result<TokenResponseDTO>> RefreshTokenAsync(RefreshRequestDTO requestDTO);
        Task<Result> LogoutAsync(RefreshRequestDTO requestDTO);
        Task<Result> ChangePasswordAsync(string userEmail, ChangePasswordDTO passwordDTO);
        Task<bool> EmailExistsAsync(string email);
        Task<Result<TokenResponseDTO>> GoogleLoginAsync(string email, string name, string providerKey);
    }
}
