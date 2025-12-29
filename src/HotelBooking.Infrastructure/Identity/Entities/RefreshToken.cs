using HotelBooking.Domain.Entities.Common;

namespace HotelBooking.Infrastructure.Identity.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}