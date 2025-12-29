using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.States.Commands.Handlers
{
    public class ToggleStateActiveCommandHandler : IRequestHandler<ToggleStateActiveCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleStateActiveCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(ToggleStateActiveCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<State>();

            var state = await repo.GetByIdAsync(request.StateId);
            if (state is null)
                return Result.Fail(Error.NotFound("State.NotFound", $"State with id {request.StateId} not found"));

            state.IsActive = !state.IsActive;

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("State.ToggleFailed", "State can't be updated"));

            return Result.Ok();
        }
    }
}
