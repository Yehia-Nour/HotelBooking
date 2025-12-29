using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomTypeSpecifications
{
    internal class RoomTypeByNameSpecification : ICriteriaSpecification<RoomType>
    {
        public Expression<Func<RoomType, bool>> Criteria { get; }

        private RoomTypeByNameSpecification(Expression<Func<RoomType, bool>> criteria) { Criteria = criteria; }

        public static RoomTypeByNameSpecification ForName(string name) => new(r => r.TypeName == name);
    }
}
