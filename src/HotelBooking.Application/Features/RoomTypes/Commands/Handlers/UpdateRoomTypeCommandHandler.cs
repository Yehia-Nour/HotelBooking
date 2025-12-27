using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Features.RoomTypes.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomTypeSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Handlers
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateRoomTypeWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(request.Command.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {request.Command.RoomTypeId} not found"));

            var spec = RoomTypeByNameSpecification.ForName(request.Command.TypeName);
            var existingRoomType = await repo.GetAsync(new List<IBaseSpecification<RoomType>> { spec });
            if (existingRoomType is not null && existingRoomType.TypeName != roomType.TypeName)
                return Result.Fail(Error.Failure("RoomType.Failure", description: $"A room type with this name {request.Command.TypeName} already exists"));

            _mapper.Map(request.Command, roomType);
            roomType.ModifiedBy = request.UserEmail;

            repo.Update(roomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.Failure", $"RoomType can't be updated"));

            return Result.Ok();
        }
    }
}
