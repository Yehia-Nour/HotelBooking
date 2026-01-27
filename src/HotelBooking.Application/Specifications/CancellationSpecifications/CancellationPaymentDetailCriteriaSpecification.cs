using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Payments;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationPaymentDetailCriteriaSpecification : ICriteriaSpecification<PaymentDetail>
    {
        public Expression<Func<PaymentDetail, bool>> Criteria { get; }

        private CancellationPaymentDetailCriteriaSpecification(Expression<Func<PaymentDetail, bool>> criteria)
            => Criteria = criteria;

        public static CancellationPaymentDetailCriteriaSpecification ByReservationRoomIds(IEnumerable<int> reservationRoomIds)
        {
            var ids = reservationRoomIds.Distinct().ToList();
            return new(pd => ids.Contains(pd.ReservationRoomID));
        }
    }
}
