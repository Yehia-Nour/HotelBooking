using HotelBooking.Domain.Contracts.Specifications;
using HotelBooking.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace HotelBooking.Application.Specifications.CancellationSpecifications
{
    public class CancellationDetailCriteriaSpecification : ICriteriaSpecification<CancellationDetail>
    {
        public Expression<Func<CancellationDetail, bool>> Criteria { get; }

        private CancellationDetailCriteriaSpecification(Expression<Func<CancellationDetail, bool>> criteria)
            => Criteria = criteria;


        public static CancellationDetailCriteriaSpecification ForReservationRoomIdsWithStatuses(
            int reservationId,
            IEnumerable<int> reservationRoomIds,
            IEnumerable<CancellationStatus> statuses)
        {
            var rrIds = reservationRoomIds.Distinct().ToList();
            var st = statuses.Distinct().ToList();

            return new(cd =>
                rrIds.Contains((int)cd.ReservationRoomId) &&
                cd.CancellationRequest.ReservationID == reservationId &&
                st.Contains(cd.CancellationRequest.CancellationStatus)
            );
        }

        public static CancellationDetailCriteriaSpecification ApprovedForReservation(int reservationId)
        {
            return new(cd =>
                cd.CancellationRequest.ReservationID == reservationId &&
                cd.CancellationRequest.CancellationStatus == CancellationStatus.Approved
            );
        }
    }
}
