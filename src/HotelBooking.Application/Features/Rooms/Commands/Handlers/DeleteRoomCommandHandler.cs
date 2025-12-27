using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Commands.Handlers
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            var room = await repo.GetByIdAsync(request.RoomId);
            if (room is null)
                return Result.Fail(Error.NotFound("Room.NotFound", $"Room with id {request.RoomId} not found"));

            var spec = ReservationsByRoomIdSpecification.ForRoomId(room.Id);
            var reservationLinkedRoom = await _unitOfWork.GetRepository<Reservation>().GetAsync(new List<IBaseSpecification<Reservation>> { spec });
            if (reservationLinkedRoom is not null)
                return Result.Fail(Error.Failure("Room.HasRelatedReservations", $"Room with id {request.RoomId} cannot be deleted because it has associated Reservations"));


            repo.Delete(room);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Room.Failure", $"Room can't be deleted"));

            return Result.Ok();
        }
    }
}
