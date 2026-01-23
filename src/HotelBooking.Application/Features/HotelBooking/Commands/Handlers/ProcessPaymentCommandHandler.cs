using HotelBooking.Application.Features.HotelBooking.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Payments;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Payments.Commands.Handlers
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessPaymentCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result<int>> Handle(ProcessPaymentWithUserCommand request, CancellationToken cancellationToken)
        {
            var req = request.Command;

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();

            var reservationCriteria = HotelBookingReservationCriteriaSpecification.ById(req.ReservationId);
            var reservationInclude = HotelBookingReservationIncludeSpecification.ReservationRooms();
            var reservation = await reservationRepo.GetAsync([reservationCriteria, reservationInclude]);
            if (reservation is null)
                return Error.Failure("Reservation.NotFound", "Reservation does not exist.");

            if (reservation.UserID != request.UserId)
                return Error.Failure("Reservation.Forbidden", "Reservation does not belong to the current user.");

            if (req.TotalAmount != reservation.TotalCost)
                return Error.Failure("Payment.TotalMismatch", "Input total amount does not match the reservation total cost.");

            if (reservation.ReservationRooms.Count == 0)
                return Error.Failure("ReservationRoom.NotFound", "No rooms found for this reservation.");

            var totalCost = reservation.TotalCost;
            var nights = reservation.NumberOfNights;

            decimal baseAmount = Math.Round(totalCost / 1.18m, 2, MidpointRounding.AwayFromZero);
            decimal gst = Math.Round(totalCost - baseAmount, 2, MidpointRounding.AwayFromZero);

            var roomIds = reservation.ReservationRooms.Select(rr => rr.RoomID).Distinct().ToList();

            var roomRepo = _unitOfWork.GetRepository<Room>();
            var rooms = await roomRepo.GetAllAsync([HotelBookingRoomCriteriaSpecification.ByIds(roomIds)]);

            var roomIdToPrice = rooms.ToDictionary(r => r.Id, r => r.Price);

            var missingRooms = roomIds.Where(id => !roomIdToPrice.ContainsKey(id)).ToList();

            if (missingRooms.Count > 0)
                return Error.Failure("Room.NotFound", $"Rooms not found: {string.Join(",", missingRooms)}");

            var paymentRepo = _unitOfWork.GetRepository<Payment>();
            var paymentDetailRepo = _unitOfWork.GetRepository<PaymentDetail>();

            var payment = new Payment
            {
                ReservationID = reservation.Id,
                Amount = baseAmount,
                GST = gst,
                TotalAmount = totalCost,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = req.PaymentMethod.Trim(),
                PaymentStatus = PaymentStatus.Pending,
            };

            await paymentRepo.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            var details = reservation.ReservationRooms.Select(rr =>
            {
                var pricePerNight = roomIdToPrice[rr.RoomID];

                var roomTotal = pricePerNight * nights;
                var roomGst = Math.Round(roomTotal * 0.18m, 2, MidpointRounding.AwayFromZero);

                return new PaymentDetail
                {
                    PaymentID = payment.Id,
                    ReservationRoomID = rr.Id,
                    Amount = pricePerNight,
                    NumberOfNights = nights,
                    GST = roomGst,
                    TotalAmount = roomTotal + roomGst,
                };
            }).ToList();

            await paymentDetailRepo.AddRangeAsync(details);
            await _unitOfWork.SaveChangesAsync();

            return payment.Id;
        }
    }
}
