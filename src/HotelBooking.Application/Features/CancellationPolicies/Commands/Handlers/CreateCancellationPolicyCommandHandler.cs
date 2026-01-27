using AutoMapper;
using HotelBooking.Application.Features.CancellationPolicies.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Commands.Handlers
{
    public class CreateCancellationPolicyCommandHandler : IRequestHandler<CreateCancellationPolicyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCancellationPolicyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCancellationPolicyCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();

            var entity = _mapper.Map<CancellationPolicy>(request);

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return entity.Id;
        }
    }
}
