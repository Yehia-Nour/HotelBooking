using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Payments;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.PaymentSpecifications
{
    public class HotelBookingPaymentIncludeSpecification : IIncludeSpecification<Payment>
    {
        public ICollection<Expression<Func<Payment, object>>> Includes { get; }

        private HotelBookingPaymentIncludeSpecification(ICollection<Expression<Func<Payment, object>>> includes)
            => Includes = includes;

        public static HotelBookingPaymentIncludeSpecification Reservation()
            => new(new List<Expression<Func<Payment, object>>> { p => p.Reservation });
    }
}
