using AutoMapper;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CountrySpecifications;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Handlers
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateCountryWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Country>();
            var country = await repo.GetByIdAsync(request.Command.CountryId);

            if (country is null)
                return Result.Fail(Error.NotFound("Country.NotFound", $"Country with id {request.Command.CountryId} not found"));

            var spec = CountryCriteriaSpecification.ByName(request.Command.CountryName);
            var existingCountry = await repo.GetAsync([spec]);
            if (existingCountry is not null && existingCountry.Id != country.Id)
                return Result.Fail(Error.Failure("Country.Failure", $"A country with name {request.Command.CountryName} already exists"));

            _mapper.Map(request.Command, country);
            country.ModifiedBy = request.UserEmail;

            repo.Update(country);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }

}
