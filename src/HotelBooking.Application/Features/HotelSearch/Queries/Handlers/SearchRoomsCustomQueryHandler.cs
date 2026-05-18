namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchRoomsCustomQueryHandler : IRequestHandler<SearchRoomsCustomQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRoomsCustomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchRoomsCustomQuery request, CancellationToken cancellationToken)
        {
            var criteriaSpec = HotelSearchCriteriaSpecification.ByCustomFilter(request.Filter);

            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([criteriaSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}
