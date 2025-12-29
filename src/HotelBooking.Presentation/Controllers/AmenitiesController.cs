using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Application.Features.Amenities.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AmenitiesController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AmenitiesController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAllAmenities(bool? isActive)
        {
            var result = await _mediator.Send(new GetAllAmenitiesQuery(isActive));

            return HandleResult(result);
        }

        [HttpGet("{amenityId}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenityById(int amenityId)
        {
            var result = await _mediator.Send(new GetAmenityByIdQuery(amenityId));

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAmenity(CreateAmenityCommand command)
        {
            var wrapper = new CreateAmenityWithUserCommand(command, GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPost("Bulk")]
        public async Task<ActionResult<int>> BulkCreateAmenity(CreateAmenitiesCommand command)
        {
            var wrapper = new CreateAmenitiesWithUserCommand(command, GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAmenity(UpdateAmenityCommand command)
        {
            var wrapper = new UpdateAmenityWithUserCommand(command, GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpDelete("{amenityId}")]
        public async Task<IActionResult> DeleteAmenity(int amenityId)
        {
            var result = await _mediator.Send(new DeleteAmenityCommand(amenityId));

            return HandleResult(result);
        }

        [HttpPut("{amenityId}")]
        public async Task<IActionResult> ToggleAmenity(int amenityId)
        {
            var result = await _mediator.Send(new ToggleAmenityActiveCommand(amenityId));

            return HandleResult(result);
        }
    }
}
