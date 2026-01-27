using AutoMapper;
using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.CancellationPolicies.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Queries.Handlers
{
    public class GetCancellationPolicyByIdQueryHandler : IRequestHandler<GetCancellationPolicyByIdQuery, Result<CancellationPolicyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCancellationPolicyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CancellationPolicyDTO>> Handle(GetCancellationPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();

            var policy = await repo.GetByIdAsync(request.Id);

            if (policy is null)
                return Error.Failure("CancellationPolicy.NotFound", "Cancellation policy not found.");

            return _mapper.Map<CancellationPolicyDTO>(policy);
        }
    }
}
