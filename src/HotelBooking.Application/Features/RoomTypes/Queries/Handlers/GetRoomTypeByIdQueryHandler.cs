using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomTypes.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.RoomTypes.Queries.Handlers
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, Result<RoomTypeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoomTypeDTO>> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.RoomTypeId);
            if (roomType is null)
                return Error.NotFound("RoomType.NotFound", $"RoomType with id {request.RoomTypeId} not found");

            return _mapper.Map<RoomTypeDTO>(roomType);
        }
    }
}
