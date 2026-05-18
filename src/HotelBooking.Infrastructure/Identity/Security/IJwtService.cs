namespace HotelBooking.Infrastructure.Identity.Security
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
