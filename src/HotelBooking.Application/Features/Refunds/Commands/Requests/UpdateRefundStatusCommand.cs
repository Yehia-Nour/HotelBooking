using HotelBooking.Domain.Entities.Payments;

namespace HotelBooking.Application.Features.Refunds.Commands.Requests
{
    public class UpdateRefundStatusCommand
    {
        public int RefundId { get; init; }
        public RefundStatus NewRefundStatus { get; init; }
    }
}
