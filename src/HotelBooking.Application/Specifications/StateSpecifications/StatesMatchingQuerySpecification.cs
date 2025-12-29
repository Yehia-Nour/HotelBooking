using HotelBooking.Application.DTOs.StateDTOs;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.StateSpecifications
{
    internal class StatesMatchingQuerySpecification : ICriteriaSpecification<State>
    {
        public Expression<Func<State, bool>> Criteria { get; }

        private StatesMatchingQuerySpecification(Expression<Func<State, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static StatesMatchingQuerySpecification ForQuery(StateQueryParams queryParams)
        {
            return new StatesMatchingQuerySpecification(s =>
                (!queryParams.CountryId.HasValue || s.CountryID == queryParams.CountryId.Value)
                && (!queryParams.IsActive.HasValue || s.IsActive == queryParams.IsActive.Value)
            );
        }
    }
}
