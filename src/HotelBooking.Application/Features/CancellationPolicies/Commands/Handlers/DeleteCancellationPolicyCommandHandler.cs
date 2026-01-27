using HotelBooking.Application.Features.CancellationPolicies.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Handlers
{
    public class DeleteCancellationPolicyCommandHandler : IRequestHandler<DeleteCancellationPolicyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCancellationPolicyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();

            var policy = await repo.GetByIdAsync(request.Id);

            if (policy is null)
                return Result.Fail(Error.Failure("CancellationPolicy.NotFound", "Cancellation policy not found."));

            repo.Delete(policy);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
