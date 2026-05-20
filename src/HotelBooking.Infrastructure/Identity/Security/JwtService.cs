namespace HotelBooking.Infrastructure.Identity.Security
{
    internal class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (JwtRegisteredClaimNames.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryInMinutes),
                claims: claims,
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
