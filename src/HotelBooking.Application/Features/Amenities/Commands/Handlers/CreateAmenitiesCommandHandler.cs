namespace HotelBooking.Application.Features.Amenities.Commands.Handlers
{
    public class CreateAmenitiesCommandHandler : IRequestHandler<CreateAmenitiesWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAmenitiesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAmenitiesWithUserCommand request, CancellationToken cancellationToken)
        {
            var amenities = _mapper.Map<List<Amenity>>(request.Command.Amenities);

            foreach (var amenity in amenities)
                amenity.CreatedBy = request.UserEmail;

            await _unitOfWork.GetRepository<Amenity>().AddRangeAsync(amenities);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
                return Error.Failure("Amenity.Failure", "Amenities can't be created");

            return result;
        }
    }

}
