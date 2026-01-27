using HotelBooking.Application.Features.Refunds.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Refunds.Commands.Handlers
{
    public class ProcessRefundWithUserCommandHandler : IRequestHandler<ProcessRefundWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessRefundWithUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(ProcessRefundWithUserCommand request, CancellationToken cancellationToken)
        {
            var processedByUserId = request.ProcessedByUserId;
            var cmd = request.Command;

            var cancellationRequestRepo = _unitOfWork.GetRepository<CancellationRequest>();

            var crCriteria = CancellationRequestCriteriaSpecification.ApprovedById(cmd.CancellationRequestId);
            var crIncludeCharge = CancellationRequestIncludeSpecification.Charge();
            var crIncludeReservation = CancellationRequestIncludeSpecification.Reservation();

            var cancellationRequest = await cancellationRequestRepo.GetAsync([
                crCriteria,
                crIncludeCharge,
                crIncludeReservation
            ]);

            if (cancellationRequest is null)
                return Error.Failure("Refund.InvalidCancellation", "Invalid CancellationRequestID or the request has not been approved.");

            var totalCost = cancellationRequest.CancellationCharge.TotalCost;
            var cancellationCharge = cancellationRequest.CancellationCharge.CancellationChargeAmount;
            var reservationId = cancellationRequest.ReservationID;

            var paymentRepo = _unitOfWork.GetRepository<Payment>();

            var payment = (await paymentRepo.GetAllAsync([HotelBookingPaymentCriteriaSpecification.ByReservationId(reservationId)])).OrderByDescending(p => p.Id).FirstOrDefault();

            if (payment is null)
                return Error.Failure("Refund.PaymentNotFound", "No payment found for the reservation.");

            var paymentId = payment.Id;

            var netRefundAmount = totalCost - cancellationCharge;
            if (netRefundAmount < 0)
                return Error.Failure("Refund.InvalidAmount", "Net refund amount cannot be negative.");

            var refundRepo = _unitOfWork.GetRepository<Refund>();

            var refund = new Refund
            {
                PaymentID = paymentId,
                RefundAmount = (decimal)netRefundAmount,
                RefundDate = DateTime.UtcNow,
                RefundReason = "Cancellation Approved",
                RefundMethodID = cmd.RefundMethodId,
                ProcessedByUserID = processedByUserId,
                RefundStatus = RefundStatus.Pending,
                CancellationCharge = (decimal)cancellationCharge,
                NetRefundAmount = netRefundAmount,
                CancellationRequestId = cmd.CancellationRequestId
            };

            await refundRepo.AddAsync(refund);
            await _unitOfWork.SaveChangesAsync();

            return refund.Id;
        }
    }
}
