namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchAvailableRoomsQueryHandler : IRequestHandler<SearchAvailableRoomsQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchAvailableRoomsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            var matchingSpec = HotelSearchCriteriaSpecification.ByAvailableWithinDates(request.Filter.CheckInDate, request.Filter.CheckOutDate);
            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([matchingSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}
