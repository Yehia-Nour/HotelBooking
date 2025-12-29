using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.StateSpecifications
{
    internal class StateByNameSpecification : ICriteriaSpecification<State>
    {
        public Expression<Func<State, bool>> Criteria { get; }

        private StateByNameSpecification(Expression<Func<State, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static StateByNameSpecification ForName(string name, int countryId)
            => new(s => s.StateName == name && s.CountryID == countryId);
    }
}
