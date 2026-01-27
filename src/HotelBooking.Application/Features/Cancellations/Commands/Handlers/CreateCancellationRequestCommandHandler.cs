using HotelBooking.Application.Features.Cancellations.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Cancellations.Commands.Handlers
{
    public class CreateCancellationRequestCommandHandler : IRequestHandler<CreateCancellationRequestWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCancellationRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateCancellationRequestWithUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var cmd = request.Command;

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();

            var reservationCriteria =
                HotelBookingReservationCriteriaSpecification.ById(cmd.ReservationId);

            var reservationInclude =
                HotelBookingReservationIncludeSpecification.ReservationRooms();

            var reservation = await reservationRepo.GetAsync([reservationCriteria, reservationInclude]);

            if (reservation is null)
                return Error.Failure("Reservation.NotFound", "No reservation found with the given ID.");

            var now = DateTime.UtcNow;

            if (reservation.Status == ReservationStatus.Cancelled || now.Date >= reservation.CheckInDate.Date)
                return Error.Failure("Cancellation.NotAllowed", "Cancellation not allowed. Reservation already fully cancelled or past check-in date.");

            var cancelledRoomIds = cmd.RoomsCancelled.Distinct().ToList();

            var reservationRoomByRoomId =
                reservation.ReservationRooms.ToDictionary(rr => rr.RoomID, rr => rr.Id);

            var invalidRoomIds = cancelledRoomIds.Where(roomId => !reservationRoomByRoomId.ContainsKey(roomId)).ToList();

            if (invalidRoomIds.Count > 0)
                return Error.Failure("Cancellation.InvalidRooms", $"These rooms are not part of the reservation: {string.Join(",", invalidRoomIds)}");

            var reservationRoomIdsToCancel = cancelledRoomIds.Select(roomId => reservationRoomByRoomId[roomId]).Distinct().ToList();


            var cancellationDetailRepo = _unitOfWork.GetRepository<CancellationDetail>();

            var alreadyCancelledOrPending = await cancellationDetailRepo.GetAllAsync([
                CancellationDetailCriteriaSpecification
                    .ForReservationRoomIdsWithStatuses(
                        cmd.ReservationId,
                        reservationRoomIdsToCancel,
                        [CancellationStatus.Pending, CancellationStatus.Approved]
                    )]);

            if (alreadyCancelledOrPending.Any())
                return Error.Failure("Cancellation.RoomAlreadyCancelledOrPending", "One or more rooms have already been cancelled or cancellation is pending.");


            var totalRooms = reservation.ReservationRooms.Count;

            var approvedDetails = await cancellationDetailRepo.GetAllAsync([
                CancellationDetailCriteriaSpecification.ApprovedForReservation(cmd.ReservationId)]);

            var cancelledRoomsApprovedCount = approvedDetails.Select(d => d.ReservationRoomId).Distinct().Count();

            var remainingRoomsCount = totalRooms - cancelledRoomsApprovedCount;

            var cancellationType = (remainingRoomsCount == reservationRoomIdsToCancel.Count) ? "Full" : "Partial";


            var cancellationRequestRepo = _unitOfWork.GetRepository<CancellationRequest>();

            var cancellationRequest = new CancellationRequest
            {
                ReservationID = cmd.ReservationId,
                UserId = userId,
                CancellationType = cancellationType,
                RequestedOn = now,
                CancellationStatus = CancellationStatus.Pending,
                CancellationReason = cmd.CancellationReason
            };

            await cancellationRequestRepo.AddAsync(cancellationRequest);
            await _unitOfWork.SaveChangesAsync();

            var cancellationRequestId = cancellationRequest.Id;

            foreach (var rrId in reservationRoomIdsToCancel)
                await cancellationDetailRepo.AddAsync(new CancellationDetail { CancellationRequestId = cancellationRequestId, ReservationRoomId = rrId });

            await _unitOfWork.SaveChangesAsync();

            return cancellationRequestId;
        }
    }
}
