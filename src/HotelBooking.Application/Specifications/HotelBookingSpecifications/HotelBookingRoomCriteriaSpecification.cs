namespace HotelBooking.Application.Specifications.HotelBookingSpecifications
{
    public class HotelBookingRoomCriteriaSpecification : ICriteriaSpecification<Room>
    {
        public Expression<Func<Room, bool>> Criteria { get; }

        private HotelBookingRoomCriteriaSpecification(Expression<Func<Room, bool>> criteria)
            => Criteria = criteria;

        public static HotelBookingRoomCriteriaSpecification ByIds(IEnumerable<int> roomIds)
        {
            var ids = roomIds.Distinct().ToList();
            return new(r => ids.Contains(r.Id));
        }

        public static HotelBookingRoomCriteriaSpecification AvailableByIdsWithinDates(
            IEnumerable<int> roomIds,
            DateTime checkIn,
            DateTime checkOut)
        {
            var ids = roomIds.Distinct().ToList();

            return new(r =>
                ids.Contains(r.Id)
                && r.IsActive
                && r.Status == BookingStatus.Available
                && !r.ReservationRooms.Any(rr =>
                    rr.CheckInDate < checkOut && rr.CheckOutDate > checkIn
                )
            );
        }
    }
}
