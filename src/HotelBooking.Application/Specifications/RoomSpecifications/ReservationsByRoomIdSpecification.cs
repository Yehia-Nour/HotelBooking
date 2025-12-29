using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.RoomSpecifications
{
    internal class ReservationsByRoomIdSpecification : ICriteriaSpecification<Reservation>
    {
        public Expression<Func<Reservation, bool>> Criteria { get; }

        private ReservationsByRoomIdSpecification(Expression<Func<Reservation, bool>> criteria)
        {
            Criteria = criteria;
        }

        public static ReservationsByRoomIdSpecification ForRoomId(int roomId)
        => new(
            r => r.RoomID == roomId &&
                 (r.Status == ReservationStatus.Reserved ||
                  r.Status == ReservationStatus.CheckedIn)
        );
    }
}
