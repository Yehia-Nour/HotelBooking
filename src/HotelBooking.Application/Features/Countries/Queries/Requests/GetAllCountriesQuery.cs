using HotelBooking.Application.DTOs.CountryDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Queries.Requests
{
    public record GetAllCountriesQuery(bool? IsActive) : IRequest<Result<IEnumerable<CountryDTO>>>;
}
