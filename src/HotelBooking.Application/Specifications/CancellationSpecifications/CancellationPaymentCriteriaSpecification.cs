using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Payments;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationPaymentCriteriaSpecification : ICriteriaSpecification<Payment>
    {
        public Expression<Func<Payment, bool>> Criteria { get; }

        private CancellationPaymentCriteriaSpecification(Expression<Func<Payment, bool>> criteria)
            => Criteria = criteria;

        public static CancellationPaymentCriteriaSpecification ByReservationId(int reservationId)
            => new(p => p.ReservationID == reservationId);
    }
}