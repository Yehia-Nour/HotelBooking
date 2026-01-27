using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.Features.Cancellations.Queries.Requests
{
    public class GetAllCancellationsRequest
    {
        public CancellationStatus? Status { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
    }
}
