using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.ReservationSpecifications
{
    internal class ReservationCriteriaSpecification : ICriteriaSpecification<Reservation>
    {
        public Expression<Func<Reservation, bool>> Criteria { get; }

        private ReservationCriteriaSpecification(Expression<Func<Reservation, bool>> criteria)
            => Criteria = criteria;

        public static ReservationCriteriaSpecification ByRoomId(int roomId)
            => new(r => r.RoomID == roomId &&
                        (r.Status == ReservationStatus.Reserved ||
                         r.Status == ReservationStatus.CheckedIn));
    }
}
