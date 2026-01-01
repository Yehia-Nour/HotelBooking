using HotelBooking.Application.Features.RoomTypes.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomTypeSpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Handlers
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomTypeCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found"));

            var spec = RoomsByRoomTypeIdSpecification.ForTypeId(request.RoomTypeId);
            var roomLinkedRoomType = await _unitOfWork.GetRepository<Room>().GetAsync([spec]);
            if (roomLinkedRoomType is not null)
                return Result.Fail(Error.Failure("RoomType.HasRelatedRooms", $"RoomType with id {request.RoomTypeId} cannot be deleted because it has associated rooms"));

            repo.Delete(roomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.Failure", $"RoomType can't be deleted"));

            return Result.Ok();
        }
    }
}
