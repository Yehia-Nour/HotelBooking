using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }

}
