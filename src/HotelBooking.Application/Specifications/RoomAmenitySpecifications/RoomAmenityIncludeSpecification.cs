using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomAmenitySpecifications
{
    internal class RoomAmenityIncludeSpecification : IIncludeSpecification<RoomAmenity>
    {
        public ICollection<Expression<Func<RoomAmenity, object>>> Includes { get; }

        private RoomAmenityIncludeSpecification(ICollection<Expression<Func<RoomAmenity, object>>> includes)
            => Includes = includes;

        public static RoomAmenityIncludeSpecification Amenity()
            => new(new List<Expression<Func<RoomAmenity, object>>> { ra => ra.Amenity });

        public static RoomAmenityIncludeSpecification RoomType()
            => new(new List<Expression<Func<RoomAmenity, object>>> { ra => ra.RoomType });
    }
}
