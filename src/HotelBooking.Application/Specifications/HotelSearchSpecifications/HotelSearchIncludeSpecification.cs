using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.HotelSearchSpecifications
{
    internal class HotelSearchIncludeSpecification : IIncludeSpecification<Room>
    {
        public ICollection<Expression<Func<Room, object>>> Includes { get; }

        private HotelSearchIncludeSpecification(ICollection<Expression<Func<Room, object>>> includes)
            => Includes = includes;

        public static HotelSearchIncludeSpecification RoomType()
            => new(new List<Expression<Func<Room, object>>> { ra => ra.RoomType });

        public static HotelSearchIncludeSpecification RoomTypeWithAmenities()
            => new(new List<Expression<Func<Room, object>>>
            {
            r => r.RoomType,
            r => r.RoomType.RoomAmenities,
            r => r.RoomType.RoomAmenities.Select(ra => ra.Amenity)
            });
    }
}
