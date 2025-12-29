using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class RoomByNumberSpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }

        private RoomByNumberSpecification(Expression<Func<Room, bool>> criteria) { Criteria = criteria; }

        public static RoomByNumberSpecification ForNumber(string number) => new(r => r.RoomNumber == number);
    }
}
