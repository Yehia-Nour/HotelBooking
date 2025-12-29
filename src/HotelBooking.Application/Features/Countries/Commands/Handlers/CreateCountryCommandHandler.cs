using AutoMapper;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CountrySpecifications;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using MediatR;

namespace HotelBooking.Application.Features.Countries.Commands.Handlers
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCountryWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Country>();

            var spec = CountryByNameSpecification.ForName(request.Command.CountryName);
            var existingCountry = await repo.GetAsync(new List<IBaseSpecification<Country>> { spec });
            if (existingCountry is not null)
                return Error.Failure("Country.Failure", $"A country with this name {request.Command.CountryName} already exists");

            var newCountry = _mapper.Map<Country>(request.Command);
            newCountry.CreatedBy = request.UserEmail;

            await repo.AddAsync(newCountry);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Country.Failure", "Country can't be created");

            return newCountry.Id;
        }
    }
}
