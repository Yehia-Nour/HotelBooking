namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchRoomsByViewTypeQueryHandler : IRequestHandler<SearchRoomsByViewTypeQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRoomsByViewTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchRoomsByViewTypeQuery request, CancellationToken cancellationToken)
        {
            var matchingSpec = HotelSearchCriteriaSpecification.ByViewType(request.ViewType);
            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([matchingSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}
