using AutoMapper;
using HotelBooking.Application.DTOs.StateDTOs;
using HotelBooking.Application.Features.States.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.States.Queries.Handlers
{
    public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, Result<StateDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<StateDTO>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<State>();
            var state = await repo.GetByIdAsync(request.StateId);

            if (state is null)
                return Error.NotFound("State.NotFound", $"State with id {request.StateId} not found");

            return _mapper.Map<StateDTO>(state);
        }
    }
}
