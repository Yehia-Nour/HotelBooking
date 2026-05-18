namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CancellationPoliciesController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public CancellationPoliciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCancellationPolicyCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCancellationPolicyCommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCancellationPolicyCommand(id));
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancellationPolicyDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCancellationPoliciesQuery());
            return HandleResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CancellationPolicyDTO>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCancellationPolicyByIdQuery(id));
            return HandleResult(result);
        }
    }
}
