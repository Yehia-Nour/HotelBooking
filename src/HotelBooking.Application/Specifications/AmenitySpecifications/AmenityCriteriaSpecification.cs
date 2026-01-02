using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.AmenitySpecifications
{
    internal class AmenityCriteriaSpecification : ICriteriaSpecification<Amenity>
    {
        public Expression<Func<Amenity, bool>> Criteria { get; }

        private AmenityCriteriaSpecification(Expression<Func<Amenity, bool>> criteria)
            => Criteria = criteria;

        public static AmenityCriteriaSpecification ForAmenityId(int amenityId)
            => new(a => a.Id == amenityId);

        public static AmenityCriteriaSpecification ForStatus(bool? isActive)
            => new(isActive is null
                ? a => true
                : a => a.IsActive == isActive.Value);
    }

}
