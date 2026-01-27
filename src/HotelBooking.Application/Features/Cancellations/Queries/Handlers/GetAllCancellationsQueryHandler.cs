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
    public class GetAllCancellationsQueryHandler : IRequestHandler<GetAllCancellationsQuery, Result<IEnumerable<CancellationRequestListItemDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCancellationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CancellationRequestListItemDTO>>> Handle(GetAllCancellationsQuery request, CancellationToken cancellationToken)
        {
            var req = request.Request;

            var repo = _unitOfWork.GetRepository<CancellationRequest>();

            var criteria = CancellationRequestCriteriaSpecification.Filter(req.Status, req.DateFrom, req.DateTo);

            var cancellations = await repo.GetAllAsync([criteria]);

            var dto = _mapper.Map<List<CancellationRequestListItemDTO>>(cancellations);

            return dto;
        }
    }
}
