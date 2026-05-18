namespace HotelBooking.Application.Features.Cancellations.Commands.Handlers
{
    public class UpdateCancellationPolicyCommandHandler : IRequestHandler<UpdateCancellationPolicyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCancellationPolicyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();

            var policy = await repo.GetByIdAsync(request.Id);

            if (policy is null)
                return Result.Fail(Error.Failure("CancellationPolicy.NotFound", "Cancellation policy not found."));

            _mapper.Map(request, policy);

            if (policy.EffectiveFromDate.HasValue
                && policy.EffectiveToDate.HasValue
                && policy.EffectiveToDate.Value.Date < policy.EffectiveFromDate.Value.Date
                )
                return Result.Fail(Error.Failure("CancellationPolicy.InvalidDates", "EffectiveToDate must be >= EffectiveFromDate."));

            repo.Update(policy);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}