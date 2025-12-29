using AutoMapper;
using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Rooms;
using MediatR;

namespace HotelBooking.Application.Features.Amenities.Commands.Handlers
{
    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAmenityWithUserCommand request, CancellationToken cancellationToken)
        {
            var newAmenity = _mapper.Map<Amenity>(request.Command);
            newAmenity.CreatedBy = request.UserEmail;

            await _unitOfWork.GetRepository<Amenity>().AddAsync(newAmenity);

            int result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Amenity.Failure", $"Amenity can't be created");

            return newAmenity.Id;
        }
    }
}
