namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class GetAmenitiesForRoomQueryHandler : IRequestHandler<GetAmenitiesForRoomQuery, Result<IEnumerable<AmenitySearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAmenitiesForRoomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AmenitySearchDTO>>> Handle(GetAmenitiesForRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(request.RoomId);
            if (room is null)
                return Error.NotFound("Room.NotFound", $"Room with id {request.RoomId} not found");

            var criteria = RoomAmenityCriteriaSpecification.ByRoomTypeId(room.RoomTypeID);
            var include = RoomAmenityIncludeSpecification.Amenity();

            var amenities = await _unitOfWork.GetRepository<RoomAmenity>().GetAllAsync([criteria, include]);

            return _mapper.Map<List<AmenitySearchDTO>>(amenities);
        }
    }
}