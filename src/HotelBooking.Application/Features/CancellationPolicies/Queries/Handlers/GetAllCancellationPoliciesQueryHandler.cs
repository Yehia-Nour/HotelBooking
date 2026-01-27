using AutoMapper;
using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.CancellationPolicies.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.CancellationPolicies.Queries.Handlers
{
    public class GetAllCancellationPoliciesQueryHandler : IRequestHandler<GetAllCancellationPoliciesQuery, Result<IEnumerable<CancellationPolicyDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCancellationPoliciesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CancellationPolicyDTO>>> Handle(GetAllCancellationPoliciesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationPolicy>();
            var list = await repo.GetAllAsync();

            var dtos = _mapper.Map<List<CancellationPolicyDTO>>(list);
            return dtos;
        }
    }
}