using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomAmenitySpecifications
{
    internal class AmenitiesByRoomTypeIdSpecification : ICriteriaSpecification<RoomAmenity>, IIncludeSpecification<RoomAmenity>
    {
        public ICollection<Expression<Func<RoomAmenity, object>>> Includes { get; }

        public Expression<Func<RoomAmenity, bool>> Criteria { get; }

        private AmenitiesByRoomTypeIdSpecification(
            ICollection<Expression<Func<RoomAmenity, object>>> includes,
            Expression<Func<RoomAmenity, bool>> criteria
            )
        {
            Includes = includes;
            Criteria = criteria;
        }

        public static AmenitiesByRoomTypeIdSpecification ForRoomType(int roomTypeId)
        {
            return new(
                criteria: ra => ra.RoomTypeID == roomTypeId,
                includes: new List<Expression<Func<RoomAmenity, object>>> { ra => ra.Amenity }
            );
        }
    }
}
