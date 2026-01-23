using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.GeographySpecifications
{
    public class HotelBookingCountryCriteriaSpecification : ICriteriaSpecification<Country>
    {
        public Expression<Func<Country, bool>> Criteria { get; }

        private HotelBookingCountryCriteriaSpecification(Expression<Func<Country, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingCountryCriteriaSpecification ActiveByIds(IEnumerable<int> countryIds)
        {
            var ids = countryIds.Distinct().ToList();

            return new(c =>
                ids.Contains(c.Id) &&
                c.IsActive
            );
        }
    }
}