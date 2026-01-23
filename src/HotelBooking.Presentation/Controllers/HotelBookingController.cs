using HotelBooking.Application.DTOs.HotelBookingDTOs;
using HotelBooking.Application.Features.HotelBooking.Commands.Requests;
using HotelBooking.Application.Features.HotelBooking.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class HotelBookingController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public HotelBookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Cost:Calculate")]
        public async Task<ActionResult<RoomCostResultDTO>> CalculateRoomCost(CalculateRoomCostRequest command)
        {
            var query = new CalculateRoomCostQuery(command);
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateReservation(CreateReservationCommand command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new CreateReservationWithUserCommand(command, userId);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPost("AddGuests")]
        public async Task<IActionResult> AddGuestsToReservation(AddGuestsToReservationCommand command)
        {
            var userId = GetUserIdFromToken();
            var userEmail = GetEmailFromToken();

            var wrapper = new AddGuestsToReservationWithUserCommand(userId, userEmail, command);

            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }


        [HttpPost("Payments:Process")]
        public async Task<ActionResult<int>> ProcessPayment(ProcessPaymentCommand command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new ProcessPaymentWithUserCommand(userId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPut("Payments:Status")]
        public async Task<IActionResult> UpdatePaymentStatus(UpdatePaymentStatusCommandRequest command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new UpdatePaymentStatusWithUserCommand(userId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }
    }
}