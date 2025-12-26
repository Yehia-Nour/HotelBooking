using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomTypes.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomTypeSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Queries.Handlers
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, Result<IEnumerable<RoomTypeDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoomTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomTypeDTO>>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var spec = RoomTypeByActiveSpecification.ForStatus(request.IsActive);

            var roomTypes = await _unitOfWork.GetRepository<RoomType>().GetAllAsync(new List<IBaseSpecification<RoomType>> { spec });

            var roomTypeDtos = _mapper.Map<List<RoomTypeDTO>>(roomTypes);

            return roomTypeDtos;
        }
    }

}
