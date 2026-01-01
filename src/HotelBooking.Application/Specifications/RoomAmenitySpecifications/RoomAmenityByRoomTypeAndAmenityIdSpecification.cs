using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomAmenitySpecifications
{
    internal class RoomAmenityByRoomTypeAndAmenityIdSpecification : ICriteriaSpecification<RoomAmenity>
    {
        public Expression<Func<RoomAmenity, bool>> Criteria { get; }

        private RoomAmenityByRoomTypeAndAmenityIdSpecification(Expression<Func<RoomAmenity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomAmenityByRoomTypeAndAmenityIdSpecification ForRoomTypeAndAmenity(int roomTypeId, int amenityId)
        {
            return new(ra => ra.RoomTypeID == roomTypeId && ra.AmenityID == amenityId);
        }
    }
}
