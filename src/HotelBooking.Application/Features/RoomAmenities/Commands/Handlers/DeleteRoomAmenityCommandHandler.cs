using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Handlers
{
    public class DeleteRoomAmenityCommandHandler : IRequestHandler<DeleteRoomAmenityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomAmenityCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteRoomAmenityCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomAmenity>();

            var spec = RoomAmenityByRoomTypeAndAmenityIdSpecification.ForRoomTypeAndAmenity(request.RoomTypeId, request.AmenityId);

            var roomAmenity = await repo.GetAsync([spec]);
            if (roomAmenity is null)
                return Result.Fail(Error.NotFound("RoomAmenity.NotFound", $"RoomAmenity with room type id {request.RoomTypeId} and amenity id {request.AmenityId} not found"));

            repo.Delete(roomAmenity);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomAmenity.Failure", $"RoomAmenity can't be deleted"));

            return Result.Ok();
        }
    }
}