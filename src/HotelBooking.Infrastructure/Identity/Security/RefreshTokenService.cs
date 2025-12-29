using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Infrastructure.Data.DbContexts;
using HotelBooking.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace HotelBooking.Infrastructure.Identity.Security
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly HotelBookingDbContext _context;

        public RefreshTokenService(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<string?> GetUserIdFromValidRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);

            if (refreshToken is null || refreshToken.ExpiryDate < DateTime.UtcNow)
                return null;

            return refreshToken.UserId;
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken is not null)
            {
                refreshToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
