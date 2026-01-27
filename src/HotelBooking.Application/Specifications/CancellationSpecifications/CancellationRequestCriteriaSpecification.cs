using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationRequestCriteriaSpecification : ICriteriaSpecification<CancellationRequest>
    {
        public Expression<Func<CancellationRequest, bool>> Criteria { get; }

        private CancellationRequestCriteriaSpecification(Expression<Func<CancellationRequest, bool>> criteria)
            => Criteria = criteria;

        public static CancellationRequestCriteriaSpecification Filter(
            CancellationStatus? status,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            return new(cr =>
                (!status.HasValue || cr.CancellationStatus == status.Value) &&
                (!dateFrom.HasValue || cr.RequestedOn >= dateFrom.Value) &&
                (!dateTo.HasValue || cr.RequestedOn <= dateTo.Value)
            );
        }

        public static CancellationRequestCriteriaSpecification ById(int id)
            => new(cr => cr.Id == id);

        public static CancellationRequestCriteriaSpecification Approved()
            => new(cr => cr.CancellationStatus == CancellationStatus.Approved);

        public static CancellationRequestCriteriaSpecification ApprovedById(int id)
          => new(cr => cr.Id == id && cr.CancellationStatus == CancellationStatus.Approved);

    }
}
