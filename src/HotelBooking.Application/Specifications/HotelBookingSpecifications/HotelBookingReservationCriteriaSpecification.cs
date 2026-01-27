using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingReservationCriteriaSpecification : ICriteriaSpecification<Reservation>
    {
        public Expression<Func<Reservation, bool>> Criteria { get; }

        private HotelBookingReservationCriteriaSpecification(Expression<Func<Reservation, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingReservationCriteriaSpecification ById(int reservationId)
            => new(r => r.Id == reservationId);

        public static HotelBookingReservationCriteriaSpecification ByIdForUser(int id, string userId)
            => new(r => r.Id == id && r.UserID == userId);
    }
}
