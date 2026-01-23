using HotelBooking.Application.Features.HotelBooking.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Application.Specifications.PaymentSpecifications;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Payments.Commands.Handlers
{
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePaymentStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdatePaymentStatusWithUserCommand request, CancellationToken cancellationToken)
        {
            var req = request.Command;

            var paymentRepo = _unitOfWork.GetRepository<Payment>();


            var paymentCriteria = HotelBookingPaymentCriteriaSpecification.ById(req.PaymentId);
            var paymentInclude = HotelBookingPaymentIncludeSpecification.Reservation();
            var payment = await paymentRepo.GetAsync([paymentCriteria, paymentInclude]);

            if (payment is null)
                return Result.Fail(Error.Failure("Payment.NotFound", "Payment record does not exist."));

            if (payment.PaymentStatus != PaymentStatus.Pending)
                return Result.Fail(Error.Failure("Payment.NotPending", "Payment status is not Pending. Cannot update."));

            if (req.NewStatus is not (PaymentStatus.Completed or PaymentStatus.Failed))
                return Result.Fail(Error.Failure("Payment.InvalidStatus", "Invalid status value. Only Completed or Failed are acceptable."));

            if (req.NewStatus == PaymentStatus.Failed && string.IsNullOrWhiteSpace(req.FailureReason))
                return Result.Fail(Error.Failure("Payment.FailureReasonRequired", "Failure reason is required when status is Failed."));

            payment.PaymentStatus = req.NewStatus;
            payment.FailureReason = req.NewStatus == PaymentStatus.Failed ? req.FailureReason?.Trim() : null;

            paymentRepo.Update(payment);

            if (req.NewStatus == PaymentStatus.Failed)
            {
                var reservation = payment.Reservation;

                if (reservation is null)
                    return Result.Fail(Error.Failure("Reservation.NotFound", "Reservation related to this payment does not exist."));

                reservation.Status = ReservationStatus.Cancelled;
                var reservationRepo = _unitOfWork.GetRepository<Reservation>();
                reservationRepo.Update(reservation);

                var rrRepo = _unitOfWork.GetRepository<ReservationRoom>();
                var reservationRoomCriteria = HotelBookingReservationRoomCriteriaSpecification.ByReservationId(reservation.Id);
                var reservationRooms = await rrRepo.GetAllAsync([reservationRoomCriteria]);

                var roomIds = reservationRooms.Select(rr => rr.RoomID).Distinct().ToList();

                if (roomIds.Count > 0)
                {
                    var roomRepo = _unitOfWork.GetRepository<Room>();
                    var rooms = await roomRepo.GetAllAsync([HotelBookingRoomCriteriaSpecification.ByIds(roomIds)]);

                    foreach (var room in rooms)
                    {
                        room.Status = BookingStatus.Available;
                        roomRepo.Update(room);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}