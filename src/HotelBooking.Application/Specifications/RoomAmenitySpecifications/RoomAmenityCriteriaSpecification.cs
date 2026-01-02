using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomAmenitySpecifications
{
    internal class RoomAmenityCriteriaSpecification : ICriteriaSpecification<RoomAmenity>
    {
        public Expression<Func<RoomAmenity, bool>> Criteria { get; }

        private RoomAmenityCriteriaSpecification(Expression<Func<RoomAmenity, bool>> criteria)
            => Criteria = criteria;

        public static RoomAmenityCriteriaSpecification ByAmenityId(int amenityId)
            => new(ra => ra.AmenityID == amenityId);

        public static RoomAmenityCriteriaSpecification ByRoomTypeId(int roomTypeId)
            => new(ra => ra.RoomTypeID == roomTypeId);

        public static RoomAmenityCriteriaSpecification ByRoomTypeIdAndAmenityId(int roomTypeId, int amenityId)
            => new(ra => ra.RoomTypeID == roomTypeId && ra.AmenityID == amenityId);
    }
}
