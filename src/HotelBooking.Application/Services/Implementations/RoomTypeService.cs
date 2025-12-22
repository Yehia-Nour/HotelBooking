using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Application.Specifications.RoomTypeSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services.Implementations
{
    internal class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomTypeDTO>>> GetAllRoomTypesAsync(bool? IsActive)
        {
            var spec = RoomTypeByActiveSpecification.ForStatus(IsActive);
            var roomTypes = await _unitOfWork.GetRepository<RoomType>().GetAllAsync(new List<IBaseSpecification<RoomType>> { spec });

            return _mapper.Map<List<RoomTypeDTO>>(roomTypes);
        }

        public async Task<Result<RoomTypeDTO>> GetRoomTypeByIdAsync(int id)
        {
            var roomType = await _unitOfWork.GetRepository<RoomType>().GetByIdAsync(id);
            if (roomType is null)
                return Error.NotFound("RoomType.NotFound", $"RoomType with id {id} not found");

            return _mapper.Map<RoomTypeDTO>(roomType);
        }

        public async Task<Result<RoomTypeDTO>> CreateRoomTypeAsync(string userEmail, CreateRoomTypeDTO roomTypeDTO)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var spec = RoomTypeByNameSpecification.ForName(roomTypeDTO.TypeName);
            var existingRoomType = await repo.GetAsync(new List<IBaseSpecification<RoomType>> { spec });
            if (existingRoomType is not null)
                return Error.Failure("RoomType.Failure", description: $"A room type with this name {roomTypeDTO.TypeName} already exists.");

            var newRoomType = _mapper.Map<RoomType>(roomTypeDTO);
            newRoomType.CreatedBy = userEmail;


            await repo.AddAsync(newRoomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("RoomType.Failure", $"RoomType can't be created");

            return _mapper.Map<RoomTypeDTO>(newRoomType);
        }

        public async Task<Result> UpdateRoomTypeAsync(string userEmail, UpdateRoomTypeDTO roomTypeDTO)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(roomTypeDTO.RoomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {roomTypeDTO.RoomTypeId} not found"));

            var spec = RoomTypeByNameSpecification.ForName(roomTypeDTO.TypeName);
            var existingRoomType = await repo.GetAsync(new List<IBaseSpecification<RoomType>> { spec });
            if (existingRoomType is not null)
                return Result.Fail(Error.Failure("RoomType.Failure", description: $"A room type with this name {roomTypeDTO.TypeName} already exists."));

            _mapper.Map(roomTypeDTO, roomType);
            roomType.ModifiedDate = DateTime.Now;
            roomType.ModifiedBy = userEmail;

            repo.Update(roomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.Failure", $"RoomType can't be updated"));

            return Result.Ok();
        }

        public async Task<Result> DeleteRoomTypeAsync(int roomTypeId)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(roomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {roomTypeId} not found"));

            var spec = RoomsByRoomTypeIdSpecification.ForTypeId(roomTypeId);
            var roomLinkedRoomType = await _unitOfWork.GetRepository<Room>().GetAsync(new List<IBaseSpecification<Room>> { spec });
            if (roomLinkedRoomType is not null)
                return Result.Fail(Error.Failure("RoomType.HasRelatedRooms", $"RoomType with id {roomTypeId} cannot be deleted because it has associated rooms"));

            repo.Delete(roomType);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.Failure", $"RoomType can't be deleted"));

            return Result.Ok();
        }

        public async Task<Result> ToggleRoomTypeActiveAsync(int roomTypeId)
        {
            var repo = _unitOfWork.GetRepository<RoomType>();

            var roomType = await repo.GetByIdAsync(roomTypeId);
            if (roomType is null)
                return Result.Fail(Error.NotFound("RoomType.NotFound", $"RoomType with id {roomTypeId} not found"));

            var spec = RoomsByRoomTypeIdSpecification.ForTypeId(roomTypeId);
            var roomLinkedRoomType = await _unitOfWork.GetRepository<Room>().GetAsync(new List<IBaseSpecification<Room>> { spec });

            if (roomType.IsActive && roomLinkedRoomType is not null)
                return Result.Fail(Error.Failure("RoomType.HasRelatedRooms", $"RoomType with id {roomTypeId} cannot be deactivated because it has associated rooms"));

            roomType.IsActive = !roomType.IsActive;

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("RoomType.ToggleFailed", $"RoomType can't be updated"));

            return Result.Ok();
        }
    }
}
