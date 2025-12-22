using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            => new  (r => r.RoomTypeID ==  roomTypeId);
    }
}
