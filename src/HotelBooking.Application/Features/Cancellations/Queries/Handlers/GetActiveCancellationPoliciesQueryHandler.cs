using AutoMapper;
using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.Cancellations.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Cancellations.Queries.Handlers
{
    public class GetActiveCancellationPoliciesQueryHandler : IRequestHandler<GetActiveCancellationPoliciesQuery, Result<IEnumerable<CancellationPolicyDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetActiveCancellationPoliciesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CancellationPolicyDTO>>> Handle(GetActiveCancellationPoliciesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();

            var criteria = CancellationPolicyCriteriaSpecification.Active();

            var policies = await repo.GetAllAsync([criteria]);

            var dtos = _mapper.Map<List<CancellationPolicyDTO>>(policies);

            return dtos;
        }
    }
}
