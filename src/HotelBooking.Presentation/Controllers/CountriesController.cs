using HotelBooking.Application.DTOs.CountryDTOs;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Features.Countries.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CountriesController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAllCountries(bool? isActive)
        {
            var result = await _mediator.Send(new GetAllCountriesQuery(isActive));
            return HandleResult(result);
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<CountryDTO>> GetCountryById(int countryId)
        {
            var result = await _mediator.Send(new GetCountryByIdQuery(countryId));
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCountry(CreateCountryCommand command)
        {
            var wrapper = new CreateCountryWithUserCommand(command, GetEmailFromToken());
            var result = await _mediator.Send(wrapper);
            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(UpdateCountryCommand command)
        {
            var wrapper = new UpdateCountryWithUserCommand(command, GetEmailFromToken());
            var result = await _mediator.Send(wrapper);
            return HandleResult(result);
        }

        [HttpDelete("{countryId}")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            var result = await _mediator.Send(new DeleteCountryCommand(countryId));
            return HandleResult(result);
        }

        [HttpPut("{countryId}/toggle")]
        public async Task<IActionResult> ToggleCountryActive(int countryId)
        {
            var result = await _mediator.Send(new ToggleCountryActiveCommand(countryId));
            return HandleResult(result);
        }
    }
}