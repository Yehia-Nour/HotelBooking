using HotelBooking.Application.Features.States.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.States.Commands.Handlers
{
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStateCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<State>();

            var state = await repo.GetByIdAsync(request.StateId);
            if (state is null)
                return Result.Fail(Error.NotFound("State.NotFound", $"State with id {request.StateId} not found"));

            repo.Delete(state);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("State.Failure", "State can't be deleted"));

            return Result.Ok();
        }
    }

}
