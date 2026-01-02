using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Geography;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CountrySpecifications
{
    internal class CountryCriteriaSpecification : ICriteriaSpecification<Country>
    {
        public Expression<Func<Country, bool>> Criteria { get; }

        private CountryCriteriaSpecification(Expression<Func<Country, bool>> criteria)
            => Criteria = criteria;

        public static CountryCriteriaSpecification ByStatus(bool? isActive)
            => new(isActive is null
                ? c => true
                : c => c.IsActive == isActive.Value);

        public static CountryCriteriaSpecification ByName(string name)
            => new(c => c.CountryName == name);
    }
}
