using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Features.RoomAmenities.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoomAmenitiesController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public RoomAmenitiesController(IMediator mediator) { _mediator = mediator; }

        [HttpGet("Amenities/{roomTypeId}")]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAllAmenitiesByRoomTypeId(int roomTypeId)
        {
            var result = await _mediator.Send(new GetAllAmenitiesByRoomTypeIdQuery(roomTypeId));

            return HandleResult(result);
        }

        [HttpGet("RoomTypes/{amenityId}")]
        public async Task<ActionResult<IEnumerable<RoomTypeDTO>>> GetAllRoomTypesByAmenityId(int amenityId)
        {
            var result = await _mediator.Send(new GetAllRoomTypesByAmenityIdQuery(amenityId));

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomAmenity(CreateRoomAmenityCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPost("Bulk")]
        public async Task<IActionResult> BulkCreateRoomAmenityToRoomType(CreateRoomAmenitiesToRoomTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("Amenities/{roomTypeId}")]
        public async Task<IActionResult> DeleteAllAmenitiesByRoomType(int roomTypeId)
        {
            var result = await _mediator.Send(new DeleteAllRoomAmenitiesByRoomTypeIdCommand(roomTypeId));

            return HandleResult(result);
        }

        [HttpDelete("/Roomtypes/{roomTypeId}/Amenities/{amenityId}")]
        public async Task<IActionResult> DeleteRoomAmenity(int roomTypeId, int amenityId)
        {
            var result = await _mediator.Send(new DeleteRoomAmenityCommand(roomTypeId, amenityId));

            return HandleResult(result);
        }
    }
}
