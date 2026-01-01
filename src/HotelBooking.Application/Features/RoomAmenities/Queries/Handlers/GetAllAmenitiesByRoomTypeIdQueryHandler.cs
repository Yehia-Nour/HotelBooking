using AutoMapper;
using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Features.RoomAmenities.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Handlers
{
    public class GetAllAmenitiesByRoomTypeIdQueryHandler : IRequestHandler<GetAllAmenitiesByRoomTypeIdQuery, IEnumerable<AmenityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAmenitiesByRoomTypeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmenityDTO>> Handle(GetAllAmenitiesByRoomTypeIdQuery request, CancellationToken cancellationToken)
        {
            var spec = AmenitiesByRoomTypeIdSpecification.ForRoomType(request.RoomTypeId);

            var amenities = await _unitOfWork.GetRepository<RoomAmenity>().GetAllAsync([spec]);

            return _mapper.Map<List<AmenityDTO>>(amenities);
        }
    }
}
