using AutoMapper;
using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Features.Rooms.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.RoomSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Rooms.Queries.Handlers
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<PaginatedResult<RoomDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoomsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<RoomDTO>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Room>();

            var matchingSpec = RoomsMatchingQuerySpecification.ForQuery(request.QueryParams);
            var sortingSpec = RoomSortingSpecification.ByOption(request.QueryParams.SortingOption);
            var paginationSpec = RoomPaginationSpecification.ForQuery(request.QueryParams);


            var rooms = await repo.GetAllAsync([matchingSpec, sortingSpec, paginationSpec]);
            var dataToReturn = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
            var countOfReturnData = dataToReturn.Count();

            var countOfAllRooms = await repo.CountAsync(new List<IBaseSpecification<Room>> { matchingSpec });

            return new PaginatedResult<RoomDTO>(request.QueryParams.PageIndex, countOfReturnData, countOfAllRooms, dataToReturn);
        }
    }
}
