namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public class CreateCountryCommand
    {
        public string CountryName { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
    }
}
