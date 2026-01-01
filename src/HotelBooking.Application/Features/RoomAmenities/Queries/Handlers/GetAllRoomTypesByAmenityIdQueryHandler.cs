using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomAmenities.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Specifications.RoomAmenitySpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomAmenities.Queries.Handlers
{
    public class GetAllRoomTypesByAmenityIdQueryHandler : IRequestHandler<GetAllRoomTypesByAmenityIdQuery, IEnumerable<RoomTypeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoomTypesByAmenityIdQueryHandler(  IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomTypeDTO>> Handle(GetAllRoomTypesByAmenityIdQuery request, CancellationToken cancellationToken)
        {
            var spec = RoomTypesByAmenityIdSpecification.ForAmenity(request.AmenityId);

            var roomTypes = await _unitOfWork.GetRepository<RoomAmenity>().GetAllAsync([spec]);

            return _mapper.Map<IEnumerable<RoomTypeDTO>>(roomTypes);
        }
    }
}
