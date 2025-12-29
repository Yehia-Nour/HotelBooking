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
    public class CreateStateCommandHandler : IRequestHandler<CreateStateWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateStateWithUserCommand request, CancellationToken cancellationToken)
        {
            var countryRepo = _unitOfWork.GetRepository<Country>();
            var country = await countryRepo.GetByIdAsync(request.Command.CountryID);
            if (country is null)
                return Error.Failure("State.Failure", $"Country with id {request.Command.CountryID} not found");

            var spec = StateByNameSpecification.ForName(request.Command.StateName, request.Command.CountryID);
            var existingState = await _unitOfWork.GetRepository<State>().GetAsync(new List<IBaseSpecification<State>> { spec });
            if (existingState is not null)
                return Error.Failure("State.Failure", $"A state with this name {request.Command.StateName} already exists in this country");

            var newState = _mapper.Map<State>(request.Command);
            newState.CreatedBy = request.UserEmail;

            await _unitOfWork.GetRepository<State>().AddAsync(newState);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("State.Failure", "State can't be created");

            return newState.Id;
        }
    }
}
