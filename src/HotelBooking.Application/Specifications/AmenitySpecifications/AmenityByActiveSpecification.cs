using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.AmenitySpecifications
{
    internal class AmenityByActiveSpecification : ICriteriaSpecification<Amenity>
    {
        public Expression<Func<Amenity, bool>> Criteria { get; }

        private AmenityByActiveSpecification(Expression<Func<Amenity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static AmenityByActiveSpecification ForStatus(bool? isActive)
        {
            if (isActive is null)
                return new(a => true);

            return new(a => a.IsActive == isActive.Value);
        }
    }
}
