using HotelBooking.Application.DTOs.CancellationDTOs;
using HotelBooking.Application.Features.Refunds.Queries.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.CancellationSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Refunds.Queries.Handlers
{
    public class GetCancellationsForRefundQueryHandler : IRequestHandler<GetCancellationsForRefundQuery, Result<IEnumerable<CancellationForRefundDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCancellationsForRefundQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CancellationForRefundDTO>>> Handle(GetCancellationsForRefundQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<CancellationRequest>();

            var criteria = CancellationRequestCriteriaSpecification.Approved();
            var include = CancellationRequestIncludeSpecification.Refunds();

            var approved = await repo.GetAllAsync([criteria, include]);

            var list = new List<CancellationForRefundDTO>();

            foreach (var cr in approved)
            {
                var latestRefund = cr.Refunds.OrderByDescending(r => r.RefundDate).FirstOrDefault();

                if (latestRefund is null)
                {
                    list.Add(new CancellationForRefundDTO
                    {
                        CancellationRequestId = cr.Id,
                        ReservationID = cr.ReservationID,
                        UserId = cr.UserId,
                        CancellationType = cr.CancellationType,
                        RequestedOn = cr.RequestedOn,
                        CancellationStatus = cr.CancellationStatus,
                        RefundId = 0,
                        RefundStatus = "Not Initiated"
                    });

                    continue;
                }

                var statusText = latestRefund.RefundStatus.ToString();

                if (statusText is "Pending" or "Failed")
                {
                    list.Add(new CancellationForRefundDTO
                    {
                        CancellationRequestId = cr.Id,
                        ReservationID = cr.ReservationID,
                        UserId = cr.UserId,
                        CancellationType = cr.CancellationType,
                        RequestedOn = cr.RequestedOn,
                        CancellationStatus = cr.CancellationStatus,
                        RefundId = latestRefund.Id,
                        RefundStatus = statusText
                    });
                }
            }

            return list;
        }
    }
}
