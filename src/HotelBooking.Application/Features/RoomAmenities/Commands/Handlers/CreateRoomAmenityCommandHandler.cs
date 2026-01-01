using AutoMapper;
using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Handlers
{
    public class CreateRoomAmenityCommandHandler : IRequestHandler<CreateRoomAmenityCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomAmenityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateRoomAmenityCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found"));

            var amenity = await _unitOfWork.GetRepository<Amenity>().GetByIdAsync(request.AmenityId);
            if (amenity is null)
                return Result.Fail(Error.NotFound("Amenity.NotFound", $"Amenity with id {request.AmenityId} not found"));

            var repo = _unitOfWork.GetRepository<RoomAmenity>();

            var spec = RoomAmenityByRoomTypeAndAmenityIdSpecification.ForRoomTypeAndAmenity(request.RoomTypeId, request.AmenityId);

            var roomAmenity = await repo.GetAsync([spec]);
            if (roomAmenity is not null)
                return Result.Fail(Error.Failure("RoomAmenity.Failure", "This amenity is already assigned to the room type"));

            var newRoomAmenity = new RoomAmenity
            {
                RoomTypeID = request.RoomTypeId,
                AmenityID = request.AmenityId
            };

            await repo.AddAsync(newRoomAmenity);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomAmenity.Failure", $"RoomAmenity can't be created"));

            return Result.Ok();
        }
    }
}
