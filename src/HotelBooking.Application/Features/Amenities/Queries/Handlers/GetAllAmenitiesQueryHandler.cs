using AutoMapper;
using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Features.Amenities.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.AmenitySpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Queries.Handlers
{
    public class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, Result<IEnumerable<AmenityDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAmenitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AmenityDTO>>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var spec = AmenityCriteriaSpecification.ForStatus(request.IsActive);

            var amenities = await _unitOfWork.GetRepository<Amenity>().GetAllAsync(new List<IBaseSpecification<Amenity>> { spec });

            var amenityDtos = _mapper.Map<List<AmenityDTO>>(amenities);

            return amenityDtos;
        }
    }
}
