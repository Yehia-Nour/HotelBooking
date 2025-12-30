using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomAmenitySpecifications
{
    internal class RoomTypesByAmenityIdSpecification : ICriteriaSpecification<RoomAmenity>, IIncludeSpecification<RoomAmenity>
    {
        public ICollection<Expression<Func<RoomAmenity, object>>> Includes { get; }
        public Expression<Func<RoomAmenity, bool>> Criteria { get; }

        private RoomTypesByAmenityIdSpecification(
            ICollection<Expression<Func<RoomAmenity, object>>> includes,
            Expression<Func<RoomAmenity, bool>> criteria)
        {
            Includes = includes;
            Criteria = criteria;
        }

        public static RoomTypesByAmenityIdSpecification ForAmenity(int amenityId)
        {
            return new(
                criteria: ra => ra.AmenityID == amenityId,
                includes: new List<Expression<Func<RoomAmenity, object>>> { ra => ra.RoomType }
            );
        }
    }
}
