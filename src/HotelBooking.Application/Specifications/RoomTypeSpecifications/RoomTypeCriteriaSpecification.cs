using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomTypeSpecifications
{
    internal class RoomTypeCriteriaSpecification : ICriteriaSpecification<RoomType>
    {
        public Expression<Func<RoomType, bool>> Criteria { get; }

        private RoomTypeCriteriaSpecification(Expression<Func<RoomType, bool>> criteria)
            => Criteria = criteria;

        public static RoomTypeCriteriaSpecification ByStatus(bool? isActive)
            => new(isActive is null
                ? r => true
                : r => r.IsActive == isActive.Value);

        public static RoomTypeCriteriaSpecification ByName(string name)
            => new(r => r.TypeName == name);
    }
}