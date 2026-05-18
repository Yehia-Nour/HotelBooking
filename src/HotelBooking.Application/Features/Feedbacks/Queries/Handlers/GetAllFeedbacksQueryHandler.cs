namespace HotelBooking.Application.Features.Feedbacks.Queries.Handlers
{
    public class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, Result<IEnumerable<FeedbackDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllFeedbacksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<FeedbackDTO>>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Feedback>();
            var all = await repo.GetAllAsync();

            var dtos = _mapper.Map<List<FeedbackDTO>>(all);
            return dtos;
        }
    }
}
