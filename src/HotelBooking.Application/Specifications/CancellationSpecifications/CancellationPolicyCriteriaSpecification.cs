using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationPolicyCriteriaSpecification : ICriteriaSpecification<CancellationPolicy>
    {
        public Expression<Func<CancellationPolicy, bool>> Criteria { get; }

        private CancellationPolicyCriteriaSpecification(Expression<Func<CancellationPolicy, bool>> criteria)
            => Criteria = criteria;

        public static CancellationPolicyCriteriaSpecification Active()
        {
            var now = DateTime.UtcNow;

            return new(p =>
                p.EffectiveFromDate <= now &&
                p.EffectiveToDate >= now
            );
        }

        public static CancellationPolicyCriteriaSpecification ActiveOnDate(DateTime date)
        {
            var d = date.Date;

            return new(p =>
                p.EffectiveFromDate <= d &&
                p.EffectiveToDate >= d
            );
        }
    }
}