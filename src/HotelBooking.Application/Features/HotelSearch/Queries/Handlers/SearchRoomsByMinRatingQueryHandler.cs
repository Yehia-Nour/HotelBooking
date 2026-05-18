namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchRoomsByMinRatingQueryHandler : IRequestHandler<SearchRoomsByMinRatingQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRoomsByMinRatingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchRoomsByMinRatingQuery request, CancellationToken cancellationToken)
        {
            var criteriaSpec = HotelSearchCriteriaSpecification.ByMinAverageRating(request.MinRating);

            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([criteriaSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}
