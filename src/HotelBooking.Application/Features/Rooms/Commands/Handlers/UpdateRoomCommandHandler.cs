using AutoMapper;
using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.Rooms.Commands.Handlers
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateRoomWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            var room = await repo.GetByIdAsync(request.Command.RoomId);
            if (room is null)
                return Result.Fail(Error.NotFound("Room.NotFound", $"Room with id {request.Command.RoomId} not found"));

            var spec = RoomByNumberSpecification.ForNumber(request.Command.RoomNumber);
            var existingRoom = await repo.GetAsync(new List<IBaseSpecification<Room>> { spec });
            if (existingRoom is not null && existingRoom.RoomNumber != room.RoomNumber)
                return Result.Fail(Error.Failure("Room.Failure", $"A room  with this number {request.Command.RoomNumber} already exists"));

            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.Command.RoomTypeID);
            if (roomType is null)
                return Result.Fail(Error.Failure("Room.Failure", $"RoomType id {request.Command.RoomTypeID} is not found"));


            _mapper.Map(request.Command, room);
            room.ModifiedBy = request.UserEmail;

            repo.Update(room);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Room.Failure", $"Room can't be updated"));

            return Result.Ok();
        }
    }
}
