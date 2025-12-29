using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CountrySpecifications
{
    public class CountryByActiveSpecification : ICriteriaSpecification<Country>
    {
        public Expression<Func<Country, bool>> Criteria { get; }

        private CountryByActiveSpecification(Expression<Func<Country, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static CountryByActiveSpecification ForStatus(bool? isActive)
        {
            if (isActive is null)
                return new(c => true);

            return new(c => c.IsActive == isActive.Value);
        }
    }
}
