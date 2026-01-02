using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Commands.Handlers
{
    public class DeleteAmenityCommandHandler : IRequestHandler<DeleteAmenityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAmenityCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Amenity>();

            var amenity = await repo.GetByIdAsync(request.AmenityId);
            if (amenity is null)
                return Result.Fail(Error.NotFound("Amenity.NotFound", $"Amenity with id {request.AmenityId} not found"));

            var spec = RoomAmenityCriteriaSpecification.ByAmenityId(request.AmenityId);
            var roomTypeLinkedAmenity = await _unitOfWork.GetRepository<RoomAmenity>().GetAsync([spec]);
            if (roomTypeLinkedAmenity is not null)
                return Result.Fail(Error.Failure("Amenity.HasRelatedRoomTypes", $"Amenity with id {request.AmenityId} cannot be deleted because it has associated RoomTypes"));

            repo.Delete(amenity);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Amenity.Failure", $"Amenity can't be deleted"));

            return Result.Ok();
        }
    }
}
