using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.Cancellations.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Cancellations.Queries.Handlers
{
    public class CalculateCancellationChargesQueryHandler
          : IRequestHandler<CalculateCancellationChargesQuery, Result<CalculateCancellationChargesResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculateCancellationChargesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CalculateCancellationChargesResultDTO>> Handle(
            CalculateCancellationChargesQuery request,
            CancellationToken cancellationToken)
        {
            var req = request.Request;

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();

            var reservationCriteria = HotelBookingReservationCriteriaSpecification.ById(req.ReservationId);
            var reservationInclude = HotelBookingReservationIncludeSpecification.ReservationRooms();

            var reservation = await reservationRepo.GetAsync([reservationCriteria, reservationInclude]);
            if (reservation is null)
                return Error.Failure("Reservation.NotFound", "No reservation found with the given ID.");

            var checkInDate = reservation.CheckInDate.Date;


            var totalRoomsCount = reservation.ReservationRooms.Count;
            if (totalRoomsCount == 0)
                return Error.Failure("Reservation.NoRooms", "Reservation has no rooms.");

            var cancelledRoomIds = req.RoomsCancelled.Distinct().ToList();
            var cancelledRoomsCount = cancelledRoomIds.Count;

            var reservationRoomByRoomId = reservation.ReservationRooms.ToDictionary(rr => rr.RoomID, rr => rr.Id);

            var invalidRoomIds = cancelledRoomIds.Where(id => !reservationRoomByRoomId.ContainsKey(id)).ToList();

            if (invalidRoomIds.Count > 0)
                return Error.Failure("Cancellation.InvalidRooms", $"These rooms are not part of the reservation: {string.Join(",", invalidRoomIds)}");

            var isFullCancellation = cancelledRoomsCount == totalRoomsCount;

            decimal totalCost;

            if (isFullCancellation)
            {
                var paymentRepo = _unitOfWork.GetRepository<Payment>();
                var payments = await paymentRepo.GetAllAsync(
                    [CancellationPaymentCriteriaSpecification.ByReservationId(req.ReservationId)]
                );

                totalCost = payments.Sum(p => p.TotalAmount);
            }
            else
            {
                var reservationRoomIds = cancelledRoomIds
                    .Select(roomId => reservationRoomByRoomId[roomId])
                    .Distinct()
                    .ToList();

                var detailsRepo = _unitOfWork.GetRepository<PaymentDetail>();
                var details = await detailsRepo.GetAllAsync([CancellationPaymentDetailCriteriaSpecification.ByReservationRoomIds(reservationRoomIds)]);

                totalCost = details.Sum(d => d.Amount);
            }

            if (totalCost <= 0)
                return Error.Failure("Cancellation.CostCalculationFailed", "Failed to calculate total costs.");

            var policyRepo = _unitOfWork.GetRepository<CancellationPolicy>();
            var policies = await policyRepo.GetAllAsync([CancellationPolicyCriteriaSpecification.ActiveOnDate(checkInDate)]);

            var policy = policies
                .OrderByDescending(p => p.EffectiveFromDate)
                .FirstOrDefault();

            if (policy is null)
                return Error.Failure("CancellationPolicy.NotFound", "No cancellation policy found for this reservation date.");

            var percentage = policy.CancellationChargePercentage;

            var charge = totalCost * (percentage / 100m);


            return new CalculateCancellationChargesResultDTO
            {
                TotalCost = decimal.Round(totalCost, 2),
                CancellationCharge = decimal.Round((decimal)charge, 2),
                CancellationPercentage = (decimal)percentage,
                PolicyDescription = policy.Description,
                IsFullCancellation = isFullCancellation
            };
        }
    }
}
