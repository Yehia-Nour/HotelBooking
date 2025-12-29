namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public class UpdateCountryCommand
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
    }
}
