namespace HotelBooking.Application.Features.States.Queries.Handlers
{
    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, Result<IEnumerable<StateDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StateDTO>>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<State>();

            var spec = StateCriteriaSpecification.ForQuery(request.QueryParams);

            var states = await repo.GetAllAsync([spec]);

            var stateDtos = _mapper.Map<List<StateDTO>>(states);

            return stateDtos;
        }
    }
}
