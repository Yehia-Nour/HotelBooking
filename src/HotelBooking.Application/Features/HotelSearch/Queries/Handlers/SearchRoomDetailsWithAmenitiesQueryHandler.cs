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
    public class SearchRoomDetailsWithAmenitiesQueryHandler : IRequestHandler<SearchRoomDetailsWithAmenitiesQuery, Result<RoomSearchWithAmenitiesDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchRoomDetailsWithAmenitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoomSearchWithAmenitiesDTO>> Handle(SearchRoomDetailsWithAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var matchingSpec = HotelSearchCriteriaSpecification.ByRoomId(request.RoomId);
            var includeSpec = HotelSearchIncludeSpecification.RoomTypeWithAmenities();

            var room = await _unitOfWork.GetRepository<Room>().GetAsync([matchingSpec, includeSpec]);
            if (room is null)
                return Error.NotFound("Room.NotFound", $"Room with id {request.RoomId} not found");

            return _mapper.Map<RoomSearchWithAmenitiesDTO>(room);
        }
    }
}
