using HotelBooking.Application.DTOs.StateDTOs;
using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Features.States.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StatesController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public StatesController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDTO>>> GetAllStates([FromQuery] StateQueryParams queryParams)
        {
            var result = await _mediator.Send(new GetAllStatesQuery(queryParams));

            return HandleResult(result);
        }

        [HttpGet("{stateId}")]
        public async Task<ActionResult<StateDTO>> GetStateById(int stateId)
        {
            var result = await _mediator.Send(new GetStateByIdQuery(stateId));

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateState(CreateStateCommand command)
        {
            var wrapper = new CreateStateWithUserCommand(
                command,
                GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateState(UpdateStateCommand command)
        {
            var wrapper = new UpdateStateWithUserCommand(
                command,
                GetEmailFromToken());

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpDelete("{stateId}")]
        public async Task<IActionResult> DeleteState(int stateId)
        {
            var result = await _mediator.Send(new DeleteStateCommand(stateId));

            return HandleResult(result);
        }

        [HttpPut("{stateId}")]
        public async Task<IActionResult> ToggleState(int stateId)
        {
            var result = await _mediator.Send(new ToggleStateActiveCommand(stateId));

            return HandleResult(result);
        }
    }
}
