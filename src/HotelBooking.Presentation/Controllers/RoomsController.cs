using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Features.Rooms.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoomsController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<RoomDTO>>> GetAllRooms([FromQuery] RoomQueryParams roomQuery)
        {
            var result = await _mediator.Send(new GetAllRoomsQuery(roomQuery));

            return HandleResult(result);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDTO>> GetRoomById(int roomId)
        {
            var result = await _mediator.Send(new GetRoomByIdQuery(roomId));

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRoom(CreateRoomCommand command)
        {
            var wrapper = new CreateRoomWithUserCommand(command, GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand command)
        {
            var wrapper = new UpdateRoomWithUserCommand(command, GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var result = await _mediator.Send(new DeleteRoomCommand(roomId));

            return HandleResult(result);
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> ToggleRoom(int roomId)
        {
            var result = await _mediator.Send(new ToggleRoomActiveCommand(roomId));

            return HandleResult(result);
        }
    }
}
