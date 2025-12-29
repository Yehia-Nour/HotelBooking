namespace HotelBooking.Application.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync(string userId);
        Task<string?> GetUserIdFromValidRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
    }
}
