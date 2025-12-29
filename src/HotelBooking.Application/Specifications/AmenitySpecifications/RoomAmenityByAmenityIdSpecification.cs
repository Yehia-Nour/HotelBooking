using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.AmenitySpecifications
{
    internal class RoomAmenityByAmenityIdSpecification : ICriteriaSpecification<RoomAmenity>
    {
        public Expression<Func<RoomAmenity, bool>> Criteria { get; }

        private RoomAmenityByAmenityIdSpecification(Expression<Func<RoomAmenity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomAmenityByAmenityIdSpecification ForAmenityId(int amenityId) => new(ra => ra.AmenityID == amenityId);
    }
}
