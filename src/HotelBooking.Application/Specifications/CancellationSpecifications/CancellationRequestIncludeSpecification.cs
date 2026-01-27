using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationRequestIncludeSpecification : IIncludeSpecification<CancellationRequest>
    {
        public ICollection<Expression<Func<CancellationRequest, object>>> Includes { get; }

        private CancellationRequestIncludeSpecification(ICollection<Expression<Func<CancellationRequest, object>>> includes)
            => Includes = includes;

        public static CancellationRequestIncludeSpecification Details()
        {
            return new(new List<Expression<Func<CancellationRequest, object>>>
            {
                cr => cr.CancellationDetails
            });
        }

        public static CancellationRequestIncludeSpecification Refunds()
        {
            return new(new List<Expression<Func<CancellationRequest, object>>>
            {
                cr => cr.Refunds
            });
        }

        public static CancellationRequestIncludeSpecification Charge()
        {
            return new(new List<Expression<Func<CancellationRequest, object>>>
            {
                cr => cr.CancellationCharge
            });
        }

        public static CancellationRequestIncludeSpecification Reservation()
        {
            return new(new List<Expression<Func<CancellationRequest, object>>>
            {
                cr => cr.Reservation
            });
        }
    }
}