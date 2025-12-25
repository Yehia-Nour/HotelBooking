using AutoMapper;
using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Application.Specifications.RoomSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services.Implementations
{
    internal class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<RoomDTO>>> GetAllRoomsAsync(RoomQueryParams queryParams)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            var matchingSpec = RoomsMatchingQuerySpecification.ForQuery(queryParams);
            var sortingSpec = RoomSortingSpecification.ByOption(queryParams.sortingOption);
            var paginationSpec = RoomPaginationSpecification.ForQuery(queryParams);

            var specList = new List<IBaseSpecification<Room>> { matchingSpec, sortingSpec, paginationSpec };

            var rooms = await repo.GetAllAsync(specList);
            var dataToReturn = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
            var countOfReturnData = dataToReturn.Count();

            var countOfAllRooms = await repo.CountAsync(new List<IBaseSpecification<Room>> { matchingSpec });

            return new PaginatedResult<RoomDTO>(queryParams.PageIndex, countOfReturnData, countOfAllRooms, dataToReturn);
        }
    }
}
