namespace HotelBooking.Infrastructure.Options
{
    public sealed class GoogleAuthOptions
    {
        public const string SectionName = "Authentication:Google";

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;
    }
}
