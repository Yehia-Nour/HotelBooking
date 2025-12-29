using AutoMapper;
using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.StateSpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.States.Commands.Handlers
{
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateStateWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<State>();

            var state = await repo.GetByIdAsync(request.Command.StateId);
            if (state is null)
                return Result.Fail(Error.NotFound("State.NotFound", $"State with id {request.Command.StateId} not found"));

            var country = await _unitOfWork.GetRepository<Country>().GetByIdAsync(request.Command.CountryID);
            if (country is null)
                return Result.Fail(Error.Failure("State.Failure", $"Country with id {request.Command.CountryID} not found"));

            var spec = StateByNameSpecification.ForName(request.Command.StateName, request.Command.CountryID);
            var existingState = await repo.GetAsync(new List<IBaseSpecification<State>> { spec });
            if (existingState is not null && existingState.Id != state.Id)
                return Result.Fail(Error.Failure("State.Failure", $"A state with this name {request.Command.StateName} already exists in this country"));

            _mapper.Map(request.Command, state);
            state.ModifiedBy = request.UserEmail;

            repo.Update(state);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("State.Failure", "State can't be updated"));

            return Result.Ok();
        }
    }
}
