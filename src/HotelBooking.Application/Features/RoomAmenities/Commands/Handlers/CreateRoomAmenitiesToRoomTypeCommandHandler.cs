using AutoMapper;
using HotelBooking.Application.Features.RoomAmenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Commands.Handlers
{
    public class CreateRoomAmenitiesToRoomTypeCommandHandler : IRequestHandler<CreateRoomAmenitiesToRoomTypeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomAmenitiesToRoomTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateRoomAmenitiesToRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found"));

            foreach (var id in request.AmenityIds)
            {
                var amenity = await _unitOfWork.GetRepository<Amenity>().GetByIdAsync(id);
                if (amenity is null)
                    return Result.Fail(Error.NotFound("Amenity.NotFound", $"Amenity with id {id} not found"));
            }

            var repo = _unitOfWork.GetRepository<RoomAmenity>();

            List<RoomAmenity> amenities = [];

            foreach (var id in request.AmenityIds)
                amenities.Add(new() { RoomTypeID = request.RoomTypeId, AmenityID = id });

            await repo.AddRangeAsync(amenities);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomAmenity.Failure", $"RoomAmenities can't be created"));

            return Result.Ok();
        }
    }
}
