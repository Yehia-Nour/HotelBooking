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
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateRoomWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Room>();


            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(request.Command.RoomTypeID);
            if (roomType is null)
                return Error.Failure("Room.Failure", $"RoomType id {request.Command.RoomTypeID} is not found");

            var spec = RoomByNumberSpecification.ForNumber(request.Command.RoomNumber);
            var existingRoom = await repo.GetAsync(new List<IBaseSpecification<Room>> { spec });
            if (existingRoom is not null)
                return Error.Failure("Room.Failure", description: $"A room  with this number {request.Command.RoomNumber} already exists");

            var newRoom = _mapper.Map<Room>(request.Command);
            newRoom.CreatedBy = request.UserEmail;


            await repo.AddAsync(newRoom);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Room.Failure", $"Room can't be created");

            return newRoom.Id;
        }
    }
}
