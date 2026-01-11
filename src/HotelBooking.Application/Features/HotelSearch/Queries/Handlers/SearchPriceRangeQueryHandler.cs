using AutoMapper;
using HotelBooking.Application.DTOs.HotelSearchDTOs;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.HotelSearchSpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.HotelSearch.Queries.Handlers
{
    public class SearchPriceRangeQueryHandler : IRequestHandler<SearchPriceRangeQuery, Result<IEnumerable<RoomSearchDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchPriceRangeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomSearchDTO>>> Handle(SearchPriceRangeQuery request, CancellationToken cancellationToken)
        {
            var matchingSpec = HotelSearchCriteriaSpecification.ByAvailableWithinPriceRange(request.Filter.MinPrice, request.Filter.MaxPrice);
            var includeSpec = HotelSearchIncludeSpecification.RoomType();

            var rooms = await _unitOfWork.GetRepository<Room>().GetAllAsync([matchingSpec, includeSpec]);

            return _mapper.Map<List<RoomSearchDTO>>(rooms);
        }
    }
}