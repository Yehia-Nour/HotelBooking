using HotelBooking.Application.Features.Refunds.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Payments;
using MediatR;

namespace HotelBooking.Application.Features.Refunds.Commands.Handlers
{
    public class UpdateRefundStatusCommandHandler : IRequestHandler<UpdateRefundStatusWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRefundStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateRefundStatusWithUserCommand request, CancellationToken cancellationToken)
        {
            var cmd = request.Command;

            var refundRepo = _unitOfWork.GetRepository<Refund>();

            var refund = await refundRepo.GetByIdAsync(cmd.RefundId);
            if (refund is null)
                return Result.Fail(Error.Failure("Refund.NotFound", "Refund not found."));

            if (refund.RefundStatus == RefundStatus.Completed)
                return Result.Fail(Error.Failure("Refund.Completed", "Refund is already completed and cannot be updated."));

            refund.RefundStatus = cmd.NewRefundStatus;

            refundRepo.Update(refund);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
