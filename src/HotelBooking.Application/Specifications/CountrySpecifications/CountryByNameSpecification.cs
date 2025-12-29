using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CountrySpecifications
{
    internal class CountryByNameSpecification : ICriteriaSpecification<Country>
    {
        public Expression<Func<Country, bool>> Criteria { get; }

        private CountryByNameSpecification(Expression<Func<Country, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static CountryByNameSpecification ForName(string name)
            => new(c => c.CountryName == name);
    }
}