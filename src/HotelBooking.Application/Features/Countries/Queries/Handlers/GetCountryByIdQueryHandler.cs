using AutoMapper;
using HotelBooking.Application.DTOs.CountryDTOs;
using HotelBooking.Application.Features.Countries.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Queries.Handlers
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Result<CountryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CountryDTO>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.GetRepository<Country>().GetByIdAsync(request.CountryId);

            if (country is null)
                return Error.NotFound("Country.NotFound", $"Country with id {request.CountryId} not found");

            return _mapper.Map<CountryDTO>(country);
        }
    }

}
