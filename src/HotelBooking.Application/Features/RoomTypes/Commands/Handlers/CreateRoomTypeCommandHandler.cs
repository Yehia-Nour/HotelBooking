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
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateRoomTypeWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var spec = RoomTypeByNameSpecification.ForName(request.Command.TypeName);
            var existingRoomType = await repo.GetAsync(new List<IBaseSpecification<RoomType>> { spec });
            if (existingRoomType is not null)
                return Error.Failure("RoomType.Failure", description: $"A room type with this name {request.Command.TypeName} already exists");

            var newRoomType = _mapper.Map<RoomType>(request.Command);
            newRoomType.CreatedBy = request.UserEmail;


            await repo.AddAsync(newRoomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("RoomType.Failure", $"RoomType can't be created");

            return newRoomType.Id;
        }
    }
}
