namespace HotelBooking.Application.Features.Countries.Queries.Handlers
{
    public class GetAllCountriesQueryHandler
        : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable<CountryDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CountryDTO>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var spec = CountryCriteriaSpecification.ByStatus(request.IsActive);

            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync([spec]);

            var countryDtos = _mapper.Map<List<CountryDTO>>(countries);

            return countryDtos;
        }
    }

}
