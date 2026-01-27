using HotelBooking.Domain.Entities.Reservations;

namespace HotelBooking.Application.Features.Cancellations.Commands.Requests
{
    public class ReviewCancellationRequestCommand
    {
        public int CancellationRequestId { get; init; }

        public CancellationStatus ApprovalStatus { get; init; }
    }
}
