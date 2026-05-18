namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchRoomsByAmenityQueryHandler : IRequestHandler<SearchRoomsByAmenityQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRoomsByAmenityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchRoomsByAmenityQuery request, CancellationToken cancellationToken)
        {
            var matchingSpec = HotelSearchCriteriaSpecification.ByAmenity(request.AmenityName);
            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([matchingSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}
