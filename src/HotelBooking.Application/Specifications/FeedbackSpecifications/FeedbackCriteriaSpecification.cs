using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.FeedbackSpecifications
{
    public class FeedbackCriteriaSpecification : ICriteriaSpecification<Feedback>
    {
        public Expression<Func<Feedback, bool>> Criteria { get; }

        private FeedbackCriteriaSpecification(Expression<Func<Feedback, bool>> criteria)
            => Criteria = criteria;

        public static FeedbackCriteriaSpecification ById(int id)
            => new(f => f.Id == id);

        public static FeedbackCriteriaSpecification ByReservationId(int reservationId)
            => new(f => f.ReservationID == reservationId);

        public static FeedbackCriteriaSpecification ByIdForUser(int feedbackId, string userId)
            => new(f => f.Id == feedbackId && f.UserId == userId);

        public static FeedbackCriteriaSpecification ByReservationForUser(int reservationId, string userId)
            => new(f => f.ReservationID == reservationId && f.UserId == userId);
    }
}
