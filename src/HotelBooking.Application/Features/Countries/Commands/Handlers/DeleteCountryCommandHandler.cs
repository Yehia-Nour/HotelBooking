using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.StateSpecifications;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Handlers
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Result> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Country>();
            var country = await repo.GetByIdAsync(request.CountryId);

            if (country is null)
                return Result.Fail(Error.NotFound("Country.NotFound", $"Country with id {request.CountryId} not found"));

            var spec = StateCriteriaSpecification.ByCountryId(request.CountryId);
            var linkedState = await _unitOfWork.GetRepository<State>().GetAsync([spec]);
            if (linkedState is not null)
                return Result.Fail(Error.Failure("Country.HasRelatedStates", $"Country with id {request.CountryId} cannot be deleted because it has associated states"));

            repo.Delete(country);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Country.Failure", "Country can't be deleted"));

            return Result.Ok();
        }
    }

}
