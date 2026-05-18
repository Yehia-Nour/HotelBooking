namespace HotelBooking.Application.Features.Countries.Queries.Requests
{
    public record GetCountryByIdQuery(int CountryId) : IRequest<Result<CountryDTO>>;
}
