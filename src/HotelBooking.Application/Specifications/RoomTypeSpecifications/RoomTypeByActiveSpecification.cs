using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomTypeSpecifications
{
    public class RoomTypeByActiveSpecification : ICriteriaSpecification<RoomType>
    {
        public Expression<Func<RoomType, bool>> Criteria { get; }

        private RoomTypeByActiveSpecification(Expression<Func<RoomType, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomTypeByActiveSpecification ForStatus(bool? isActive)
        {
            if (isActive is null)
                return new(r => true);

            return new(r => r.IsActive == isActive.Value);
        }
    }
}
