using HotelBooking.Application.Features.RoomTypes.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomTypeSpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Handlers
{
    public class ToggleRoomTypeActiveCommandHandler : IRequestHandler<ToggleRoomTypeActiveCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleRoomTypeActiveCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(ToggleRoomTypeActiveCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found"));

            var spec = RoomsByRoomTypeIdSpecification.ForTypeId(request.RoomTypeId);
            var roomLinkedRoomType = await _unitOfWork.GetRepository<Room>().GetAsync([spec]);

            if (roomType.IsActive && roomLinkedRoomType is not null)
                return Result.Fail(Error.Failure("RoomType.HasRelatedRooms", "RoomType cannot be deactivated because it has associated rooms"));

            roomType.IsActive = !roomType.IsActive;

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.ToggleFailed", "RoomType can't be updated"));

            return Result.Ok();
        }
    }
}
