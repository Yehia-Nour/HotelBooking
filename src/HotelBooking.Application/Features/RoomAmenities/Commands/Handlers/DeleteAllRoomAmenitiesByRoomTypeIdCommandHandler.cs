using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Handlers
{
    public class DeleteAllRoomAmenitiesByRoomTypeIdCommandHandler : IRequestHandler<DeleteAllRoomAmenitiesByRoomTypeIdCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAllRoomAmenitiesByRoomTypeIdCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteAllRoomAmenitiesByRoomTypeIdCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomAmenity>();

            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found"));

            var spec = RoomAmenityCriteriaSpecification.ByRoomTypeId(request.RoomTypeId);

            var amenities = await repo.GetAllAsync([spec]);
            if (!amenities.Any())
                return Result.Ok();

            foreach (var amenity in amenities)
                repo.Delete(amenity);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomAmenity.Failure", $"RoomAmenities can't be deleted"));

            return Result.Ok();
        }
    }
}
