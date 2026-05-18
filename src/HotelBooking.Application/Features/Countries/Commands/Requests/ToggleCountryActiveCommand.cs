namespace HotelBooking.Application.Features.Countries.Commands.Requests
{
    public record ToggleCountryActiveCommand(int CountryId) : IRequest<Result>;
}
