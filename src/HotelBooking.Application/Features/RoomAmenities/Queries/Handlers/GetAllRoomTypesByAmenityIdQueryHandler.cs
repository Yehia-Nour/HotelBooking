using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomAmenities.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Handlers
{
    public class GetAllRoomTypesByAmenityIdQueryHandler : IRequestHandler<GetAllRoomTypesByAmenityIdQuery, Result<IEnumerable<RoomTypeDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoomTypesByAmenityIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomTypeDTO>>> Handle(GetAllRoomTypesByAmenityIdQuery request, CancellationToken cancellationToken)
        {
            var criteria = RoomAmenityCriteriaSpecification.ByAmenityId(request.AmenityId);
            var include = RoomAmenityIncludeSpecification.RoomType();

            var roomTypes = await _unitOfWork.GetRepository<RoomAmenity>().GetAllAsync([criteria, include]);

            return _mapper.Map<List<RoomTypeDTO>>(roomTypes);
        }
    }
}
