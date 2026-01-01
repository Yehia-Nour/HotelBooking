using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Commands.Handlers
{
    public class ToggleRoomActiveCommandHandler : IRequestHandler<ToggleRoomActiveCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleRoomActiveCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(ToggleRoomActiveCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            var room = await repo.GetByIdAsync(request.RoomId);
            if (room is null)
                return Result.Fail(Error.NotFound("Room.NotFound", $"Room with id {request.RoomId} not found"));

            var spec = ReservationsByRoomIdSpecification.ForRoomId(room.Id);
            var reservationLinkedRoom = await _unitOfWork.GetRepository<Reservation>().GetAsync([spec]);
            if (room.IsActive && reservationLinkedRoom is not null)
                return Result.Fail(Error.Failure("Room.HasRelatedReservations", $"Room with id {request.RoomId} cannot be deactivated because it has associated Reservations"));


            room.IsActive = !room.IsActive;

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Room.ToggleFailed", "Room can't be updated"));

            return Result.Ok();
        }
    }
}
