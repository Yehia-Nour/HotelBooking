using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class RoomCriteriaSpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }

        private RoomCriteriaSpecification(Expression<Func<Room, bool>> criteria)
            => Criteria = criteria;

        public static RoomCriteriaSpecification ByRoomTypeId(int roomTypeId)
            => new(r => r.RoomTypeID == roomTypeId);

        public static RoomCriteriaSpecification ByRoomNumber(string number)
            => new(r => r.RoomNumber == number);

        public static RoomCriteriaSpecification MatchingQuery(RoomQueryParams queryParams)
            => new(r =>
                (!queryParams.RoomTypeId.HasValue || r.RoomTypeID == queryParams.RoomTypeId.Value)
                && (string.IsNullOrEmpty(queryParams.SearchByRoomNumber)
                    || r.RoomNumber.Contains(queryParams.SearchByRoomNumber))
                && (!queryParams.Status.HasValue || r.Status == (BookingStatus)queryParams.Status.Value)
            );
    }
}

