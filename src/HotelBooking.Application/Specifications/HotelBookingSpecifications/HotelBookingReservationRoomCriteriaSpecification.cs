namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingReservationRoomCriteriaSpecification : ICriteriaSpecification<ReservationRoom>
    {
        public Expression<Func<ReservationRoom, bool>> Criteria { get; }

        private HotelBookingReservationRoomCriteriaSpecification(Expression<Func<ReservationRoom, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingReservationRoomCriteriaSpecification ByReservationId(int reservationId)
            => new(rr => rr.ReservationID == reservationId);

        public static HotelBookingReservationRoomCriteriaSpecification ByIds(IEnumerable<int?> reservationRoomIds)
        {
            var ids = reservationRoomIds.Distinct().ToList();
            return new(rr => ids.Contains(rr.Id));
        }
    }
}
