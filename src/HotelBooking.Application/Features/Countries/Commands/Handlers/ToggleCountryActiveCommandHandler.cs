using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CountrySpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Handlers
{
    public class ToggleCountryActiveCommandHandler : IRequestHandler<ToggleCountryActiveCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleCountryActiveCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ToggleCountryActiveCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Country>();

            var country = await repo.GetByIdAsync(request.CountryId);
            if (country is null)
                return Result.Fail(Error.NotFound("Country.NotFound", $"Country with id {request.CountryId} not found"));

            var spec = StatesByCountryIdSpecification.ForCountryId(request.CountryId);
            var linkedState = await _unitOfWork.GetRepository<State>().GetAsync(new List<IBaseSpecification<State>> { spec });

            if (country.IsActive && linkedState is not null)
                return Result.Fail(Error.Failure("Country.HasRelatedStates", "Country cannot be deactivated because it has associated states"));

            country.IsActive = !country.IsActive;

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Result.Fail(Error.Failure("Country.ToggleFailed", "Country can't be updated"));

            return Result.Ok();
        }
    }
}
