using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomTypeSpecifications
{
    public class RoomsByRoomTypeIdSpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }

        private RoomsByRoomTypeIdSpecification(Expression<Func<Room, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomsByRoomTypeIdSpecification ForTypeId(int roomTypeId)
            => new(r => r.RoomTypeID == roomTypeId);
    }
}
