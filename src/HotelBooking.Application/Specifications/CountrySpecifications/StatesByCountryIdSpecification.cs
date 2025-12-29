using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CountrySpecifications
{
    internal class StatesByCountryIdSpecification : ICriteriaSpecification<State>
    {
        public Expression<Func<State, bool>> Criteria { get; }

        private StatesByCountryIdSpecification(Expression<Func<State, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static StatesByCountryIdSpecification ForCountryId(int countryId)
            => new(s => s.CountryID == countryId);
    }
}
