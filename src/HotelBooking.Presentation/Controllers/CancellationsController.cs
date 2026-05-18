namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class CancellationsController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public CancellationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancellationPolicyDTO>>> GetActiveCancellationPolicies()
        {
            var result = await _mediator.Send(new GetActiveCancellationPoliciesQuery());

            return HandleResult(result);
        }

        [HttpPost("Charges:Calculate")]
        public async Task<ActionResult<CalculateCancellationChargesResultDTO>> CalculateCharges(CalculateCancellationChargesRequest request)
        {
            var result = await _mediator.Send(new CalculateCancellationChargesQuery(request));

            return HandleResult(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> CreateCancellationRequest(CreateCancellationRequestCommand command)
        {
            var userId = GetUserIdFromToken();

            var wrapper = new CreateCancellationRequestWithUserCommand(userId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Requests")]
        public async Task<ActionResult<IEnumerable<CancellationRequestListItemDTO>>> GetAll(GetAllCancellationsRequest request)
        {
            var query = new GetAllCancellationsQuery(request);
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("Requests:Review")]
        public async Task<IActionResult> Review(ReviewCancellationRequestCommand command)
        {
            var adminId = GetUserIdFromToken();

            var wrapper = new ReviewCancellationRequestWithAdminCommand(adminId, command);
            var result = await _mediator.Send(wrapper);

            return HandleResult(result);
        }
    }
}