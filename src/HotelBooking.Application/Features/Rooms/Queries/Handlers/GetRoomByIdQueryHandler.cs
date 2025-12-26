using AutoMapper;
using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.Rooms.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Queries.Handlers
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoomDTO>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(request.Id);

            if (room is null)
                return Error.NotFound("Room.NotFound", $"Room with id {request.Id} not found");

            return _mapper.Map<RoomDTO>(room);
        }
    }
}
