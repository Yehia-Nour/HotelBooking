using HotelBooking.Application.Features.Cancellations.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Cancellations.Commands.Handlers
{
    public class ReviewCancellationRequestCommandHandler : IRequestHandler<ReviewCancellationRequestWithAdminCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewCancellationRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReviewCancellationRequestWithAdminCommand request, CancellationToken cancellationToken)
        {
            var adminId = request.AdminUserId;
            var cmd = request.Command;

            var cancellationRequestRepo = _unitOfWork.GetRepository<CancellationRequest>();

            var crCriteria = CancellationRequestCriteriaSpecification.ById(cmd.CancellationRequestId);
            var crInclude = CancellationRequestIncludeSpecification.Details();

            var cancellationRequest = await cancellationRequestRepo.GetAsync([crCriteria, crInclude]);

            if (cancellationRequest is null)
                return Result.Fail(Error.Failure("CancellationRequest.NotFound", "Cancellation request does not exist."));

            if (cancellationRequest.CancellationStatus != CancellationStatus.Pending)
                return Result.Fail(Error.Failure("CancellationRequest.NotPending", "Cancellation request is already reviewed."));

            var now = DateTime.UtcNow;

            cancellationRequest.CancellationStatus = cmd.ApprovalStatus;
            cancellationRequest.AdminReviewedById = adminId;
            cancellationRequest.ReviewDate = now;

            cancellationRequestRepo.Update(cancellationRequest);

            if (cmd.ApprovalStatus == CancellationStatus.Approved)
            {

                var reservationRoomIds = cancellationRequest.CancellationDetails.Select(d => d.ReservationRoomId).Distinct().ToList();

                if (reservationRoomIds.Count == 0)
                    return Result.Fail(Error.Failure("CancellationRequest.NoRooms", "Cancellation request has no rooms."));

                var reservationId = cancellationRequest.ReservationID;

                var reservationRoomRepo = _unitOfWork.GetRepository<ReservationRoom>();
                var rrCriteria = HotelBookingReservationRoomCriteriaSpecification.ByIds(reservationRoomIds);

                var reservationRooms = await reservationRoomRepo.GetAllAsync([rrCriteria]);
                var roomIdsCancelled = reservationRooms.Select(rr => rr.RoomID).Distinct().ToList();

                var calc = await CalculateCancellationChargesAsync(reservationId, roomIdsCancelled, cancellationToken);
                if (calc.IsFailure)
                    return Result.Fail(Error.Failure("CancellationCharge.CalculationFailed"));

                cancellationRequest.CancellationCharge = new CancellationCharge
                {
                    CancellationRequestId = cancellationRequest.Id,
                    TotalCost = calc.Value.TotalCost,
                    CancellationChargeAmount = calc.Value.CancellationCharge,
                    CancellationPercentage = calc.Value.CancellationPercentage,
                    PolicyDescription = calc.Value.PolicyDescription
                };

                var roomRepo = _unitOfWork.GetRepository<Room>();
                var roomCriteria = HotelBookingRoomCriteriaSpecification.ByIds(roomIdsCancelled);

                var rooms = await roomRepo.GetAllAsync([roomCriteria]);

                foreach (var room in rooms)
                {
                    room.Status = BookingStatus.Available;
                    roomRepo.Update(room);
                }

                var allReservationRooms = await reservationRoomRepo.GetAllAsync([HotelBookingReservationRoomCriteriaSpecification.ByReservationId(reservationId)]);

                var isFullCancel = allReservationRooms.Select(x => x.RoomID).Distinct().Count() == roomIdsCancelled.Distinct().Count();

                var reservationRepo = _unitOfWork.GetRepository<Reservation>();
                var reservation = await reservationRepo.GetByIdAsync(reservationId);

                if (reservation is null)
                    return Result.Fail(Error.Failure("Reservation.NotFound", "Reservation not found for this cancellation request."));

                if (isFullCancel)
                    reservation.Status = ReservationStatus.Cancelled;
                else
                    reservation.Status = ReservationStatus.Reserved;

                reservationRepo.Update(reservation);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private async Task<Result<CalcChargesInternal>> CalculateCancellationChargesAsync(int reservationId, List<int> roomIdsCancelled, CancellationToken cancellationToken)
        {

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();
            var reservation = await reservationRepo.GetByIdAsync(reservationId);

            if (reservation is null)
                return Error.Failure("Reservation.NotFound", "No reservation found with the given ID.");

            var checkInDate = reservation.CheckInDate.Date;

            var reservationRoomRepo = _unitOfWork.GetRepository<ReservationRoom>();
            var allRooms = await reservationRoomRepo.GetAllAsync([HotelBookingReservationRoomCriteriaSpecification.ByReservationId(reservationId)]);

            var totalRoomsCount = allRooms.Select(x => x.RoomID).Distinct().Count();
            var cancelledRoomsCount = roomIdsCancelled.Distinct().Count();

            var isFullCancellation = cancelledRoomsCount == totalRoomsCount;

            decimal totalCost;

            if (isFullCancellation)
            {
                var paymentRepo = _unitOfWork.GetRepository<Payment>();
                var payments = await paymentRepo.GetAllAsync([HotelBookingPaymentCriteriaSpecification.ByReservationId(reservationId)]);

                totalCost = payments.Sum(p => p.TotalAmount);
            }
            else
            {
                var reservationRoomIds = allRooms
                    .Where(rr => roomIdsCancelled.Contains(rr.RoomID))
                    .Select(rr => rr.Id)
                    .Distinct()
                    .ToList();

                var detailsRepo = _unitOfWork.GetRepository<PaymentDetail>();
                var details = await detailsRepo.GetAllAsync([HotelBookingPaymentDetailCriteriaSpecification.ByReservationRoomIds(reservationRoomIds)]);

                totalCost = details.Sum(d => d.Amount);
            }

            if (totalCost <= 0)
                return Error.Failure("Cancellation.CostCalculationFailed", "Failed to calculate total costs.");

            var policyRepo = _unitOfWork.GetRepository<CancellationPolicy>();
            var policies = await policyRepo.GetAllAsync([CancellationPolicyCriteriaSpecification.ActiveOnDate(checkInDate)]);

            var policy = policies.OrderByDescending(p => p.EffectiveFromDate).FirstOrDefault();
            if (policy is null)
                return Error.Failure("CancellationPolicy.NotFound", "No cancellation policy found for this reservation date.");

            var percentage = policy.CancellationChargePercentage;
            var charge = totalCost * (percentage / 100m);

            return new CalcChargesInternal
            {
                TotalCost = decimal.Round(totalCost, 2),
                CancellationCharge = decimal.Round((decimal)charge, 2),
                CancellationPercentage = (decimal)percentage,
                PolicyDescription = policy.Description
            };
        }

        private sealed record CalcChargesInternal
        {
            public decimal TotalCost { get; init; }
            public decimal CancellationCharge { get; init; }
            public decimal CancellationPercentage { get; init; }
            public string PolicyDescription { get; init; } = default!;
        }
    }
}
