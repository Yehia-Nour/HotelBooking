using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Payments;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingPaymentCriteriaSpecification : ICriteriaSpecification<Payment>
    {
        public Expression<Func<Payment, bool>> Criteria { get; }

        private HotelBookingPaymentCriteriaSpecification(Expression<Func<Payment, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingPaymentCriteriaSpecification ById(int paymentId)
            => new(p => p.Id == paymentId);

        public static HotelBookingPaymentCriteriaSpecification ByReservationId(int reservationId)
            => new(p => p.ReservationID == reservationId);
    }
}