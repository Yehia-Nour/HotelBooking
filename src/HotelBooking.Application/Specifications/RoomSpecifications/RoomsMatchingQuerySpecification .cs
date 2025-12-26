using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    public class RoomsMatchingQuerySpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }

        private RoomsMatchingQuerySpecification(Expression<Func<Room, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static RoomsMatchingQuerySpecification ForQuery(RoomQueryParams queryParams)
        {
            return new(r =>
                (!queryParams.RoomTypeId.HasValue || r.RoomTypeID == queryParams.RoomTypeId.Value)
                && (string.IsNullOrEmpty(queryParams.SearchByRoomNumber)
                    || r.RoomNumber.Contains(queryParams.SearchByRoomNumber))
                && (!queryParams.Status.HasValue || r.Status == (BookingStatus)queryParams.Status.Value)
            );
        }

    }
}
