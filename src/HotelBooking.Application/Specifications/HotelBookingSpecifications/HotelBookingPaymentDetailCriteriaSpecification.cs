namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingPaymentDetailCriteriaSpecification : ICriteriaSpecification<PaymentDetail>
    {
        public Expression<Func<PaymentDetail, bool>> Criteria { get; }

        private HotelBookingPaymentDetailCriteriaSpecification(Expression<Func<PaymentDetail, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingPaymentDetailCriteriaSpecification ByReservationRoomIds(IEnumerable<int> reservationRoomIds)
        {
            var ids = reservationRoomIds.Distinct().ToList();
            return new(pd => ids.Contains(pd.ReservationRoomID));
        }
    }
}
