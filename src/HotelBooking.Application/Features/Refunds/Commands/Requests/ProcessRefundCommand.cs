namespace HotelBooking.Application.Features.Refunds.Commands.Requests
{
    public class ProcessRefundCommand
    {
        public int CancellationRequestId { get; init; }
        public int RefundMethodId { get; init; }
    }
}
