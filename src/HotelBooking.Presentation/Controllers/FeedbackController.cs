using HotelBooking.Application.DTOs.FeedbackDTOs;
using HotelBooking.Application.Features.Feedbacks.Commands.Requests;
using HotelBooking.Application.Features.Feedbacks.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class FeedbackController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFeedbackCommand command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new CreateFeedbackWithUserCommand(userId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeedbackCommand command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new UpdateFeedbackWithUserCommand(userId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new DeleteFeedbackWithUserCommand(userId, id);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FeedbackDTO>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFeedbackByIdQuery(id));
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllFeedbacksQuery());
            return HandleResult(result);
        }
    }
}
