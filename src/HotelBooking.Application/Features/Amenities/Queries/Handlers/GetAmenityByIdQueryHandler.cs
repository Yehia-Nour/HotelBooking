using AutoMapper;
using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Features.Amenities.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Queries.Handlers
{
    public class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, Result<AmenityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAmenityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AmenityDTO>> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            var amenity = await _unitOfWork.GetRepository<Amenity>().GetByIdAsync(request.AmenityId);
            if (amenity is null)
                return Error.NotFound("Amenity.NotFound", $"Amenity with id {request.AmenityId} not found");

            return _mapper.Map<AmenityDTO>(amenity);
        }
    }
}
