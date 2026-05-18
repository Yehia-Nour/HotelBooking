namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RefundsController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public RefundsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Refunds:Pending")]
        public async Task<ActionResult<IEnumerable<CancellationForRefundDTO>>> GetCancellationsForRefund()
        {
            var result = await _mediator.Send(new GetCancellationsForRefundQuery());
            return HandleResult(result);
        }

        [HttpPost("Refunds:Process")]
        public async Task<ActionResult<int>> ProcessRefund(ProcessRefundCommand command)
        {
            var adminId = GetUserIdFromToken();

            var wrapper = new ProcessRefundWithUserCommand(adminId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [HttpPut("Refunds:Status")]
        public async Task<IActionResult> UpdateRefundStatus(UpdateRefundStatusCommand command)
        {
            var adminId = GetUserIdFromToken();

            var wrapper = new UpdateRefundStatusWithUserCommand(adminId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }
    }
}
